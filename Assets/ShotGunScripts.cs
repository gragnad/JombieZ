using UnityEngine;
using System.Collections;

public class ShotGunScripts : ItemScripts
{

   
    bool DelayCheckOk = false;
    bool ShotGunShot = false;
    // Use this for initialization
    void Start () {
        GunDemeged = 0.8f;
        ItemNumber = 3;
        //BulletCount = 500;
        BulletPlus = 30;
        Find_E_Parent();
        FindCam();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.R))
        {
            ReloadBullt(30);
        }

        FindPlayer();

        if (transform.parent != null)
        {
            if (transform.parent.Equals(E_check.transform)
          && SIngleTonData.instance.ShotCheck == true
          && SIngleTonData.instance.Reload == false)
            {   
                if(ShotGunShot == false)
                {
                    for (int i = 0; i < 10; i++)
                    {   

                        CreateBullet();
                        BulletPlus--;
                        if (BulletPlus <= 0)
                        {
                            BulletPlus = 0;
                        }
                        BulletCount--;

                    }
                    ShotGunShot = true;
                    StartCoroutine(SgotGunDelay());
                }
               
            }
        }
    }

    IEnumerator SgotGunDelay()
    {
        yield return new WaitForSeconds(1.0f);

        ShotGunShot = false;
       
    }

    protected override void CreateBullet()
    {
        if (BulletCount <= 0)
        {
            base.CreateBullet();
        }
        else if (BulletCount > 0)
        {

            Vector3 ForwardVector = Player.transform.forward * 1.0f;
            Vector3 BasicVec = Player.transform.position;

            //CopyPos = new Vector3(BasicVec.x + ForwardVector.x, BasicVec.y + ForwardVector.y + 0.8f, BasicVec.z + ForwardVector.z);
            CopyPos = transform.position;
            CopyPos += transform.forward * 0.8f;
            Quaternion chekcRot = Player.transform.rotation;
            if (BulletPlus > 0 && SIngleTonData.instance.Reload == false)
            {
                SIngleTonData.instance.g_BulletData[BulletPlus-1].transform.position = CopyPos;
                SIngleTonData.instance.g_BulletData[BulletPlus-1].transform.rotation = chekcRot;
                SIngleTonData.instance.g_BulletData[BulletPlus-1].GetComponent<GunBulletSCripts>().BulletStart = true;


                //
                Vector3 x = Random.insideUnitSphere;

                //카메라가 보는 시점으로 총알이 날라가겠끔 이거 뺴면 앞으로 날아감
                ray = cam.ScreenPointToRay(Input.mousePosition);
                Vector3 check = ray.GetPoint(10.0f);
                SIngleTonData.instance.g_BulletData[BulletPlus - 1].transform.LookAt(new Vector3(check.x + x.x, check.y + x.y, check.z));
                                         
            }
            else if (BulletPlus <= 0)
            {
                SIngleTonData.instance.Reload = true;
                StartCoroutine(RESetBullet(30));
            }
        }
    }

   

}

