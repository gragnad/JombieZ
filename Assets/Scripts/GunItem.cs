using UnityEngine;
using System.Collections;

public class GunItem : ItemScripts
{
    
    // Use this for initialization
    void Start () {
        GunDemeged = 1.5f;
        ItemNumber = 0;
        //BulletCount = 500;
        BulletPlus = 30;
        Find_E_Parent();
        FindCam();
       

    }
	
	// Update is called once per frame
	void Update () {

        FindPlayer();

        if (Input.GetKeyUp(KeyCode.R))
        {
            ReloadBullt(30);
        }

        if (transform.parent != null)
        {
            if (transform.parent.Equals(E_check.transform)
          && SIngleTonData.instance.ShotCheck == true
          && SIngleTonData.instance.Reload == false)
            {
                CreateBullet();
                BulletPlus--;
                if(BulletPlus <= 0)
                {
                    BulletPlus = 0;
                }
                BulletCount--;
            }
        }
        
  }



    protected override void CreateBullet()
    {
        if(BulletCount <= 0)
        {
            base.CreateBullet();
        }
       else if(BulletCount > 0)
        {         

            Vector3 ForwardVector = Player.transform.forward * 1.0f;
            Vector3 BasicVec = Player.transform.position;

            //CopyPos = new Vector3(BasicVec.x + ForwardVector.x, BasicVec.y + ForwardVector.y + 0.8f, BasicVec.z + ForwardVector.z);
            CopyPos = transform.position;
            CopyPos += transform.forward * 0.8f;
            Quaternion chekcRot = Player.transform.rotation;
            if(BulletPlus > 0 && SIngleTonData.instance.Reload == false)
            {
                SIngleTonData.instance.g_BulletData[BulletPlus-1].transform.position = CopyPos;
                SIngleTonData.instance.g_BulletData[BulletPlus-1].transform.rotation = chekcRot;
                SIngleTonData.instance.g_BulletData[BulletPlus-1].GetComponent<GunBulletSCripts>().BulletStart = true;
               

                //카메라가 보는 시점으로 총알이 날라가겠끔 이거 뺴면 앞으로 날아감
                ray = cam.ScreenPointToRay(Input.mousePosition);
                SIngleTonData.instance.g_BulletData[BulletPlus-1].transform.LookAt(ray.GetPoint(10.0f));
               
            }
            else if(BulletPlus <= 0)
            {                 
                SIngleTonData.instance.Reload = true;
                StartCoroutine(RESetBullet(30));
            }
        }
    }

   

}
