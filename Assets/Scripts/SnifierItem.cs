using UnityEngine;
using System.Collections;

public class SnifierItem : ItemScripts
{
    bool DelayCheckOk = false;
    // Use this for initialization
    void Start () {
        GunDemeged = 10.0f;
        ItemNumber = 2;
        Find_E_Parent();
        FindCam();
        BulletPlus = 10;
    }
	
	// Update is called once per frame
	void Update () {

        FindPlayer();

        if (Input.GetKeyUp(KeyCode.R))
        {
            ReloadBullt(10);
        }

        if (transform.parent != null)
        {
            if (transform.parent.Equals(E_check.transform)
          && SIngleTonData.instance.ShotCheck == true
          && SIngleTonData.instance.Reload == false)
            {
                if(DelayCheckOk == false)
                {
                    SIngleTonData.instance.SniperCheck = true;
                    CreateBullet();
                    BulletPlus--;
                    if (BulletPlus <= 0)
                    {
                        BulletPlus = 0;
                    }
                    BulletCount--;                   
                    DelayCheckOk = true;
                    StartCoroutine(SniperDelay());
                }              
            }
        }
    }
    IEnumerator SniperDelay()
    {
        yield return new WaitForSeconds(1.0f);

        DelayCheckOk = false;
        SIngleTonData.instance.SniperCheck = false;
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


                //카메라가 보는 시점으로 총알이 날라가겠끔
                ray = cam.ScreenPointToRay(Input.mousePosition);
                SIngleTonData.instance.g_BulletData[BulletPlus-1].transform.LookAt(ray.GetPoint(10.0f));
               
            }
            else if (BulletPlus <= 0)
            {
                SIngleTonData.instance.Reload = true;
                StartCoroutine(RESetBullet(10));
            }
        }
    }
    
}
