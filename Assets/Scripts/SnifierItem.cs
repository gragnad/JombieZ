using UnityEngine;
using System.Collections;

public class SnifierItem : ItemScripts
{
    public GameObject SniperBulletObject;

    bool DelayCheckOk = false;
    // Use this for initialization
    void Start () {
        ItemNumber = 2;
        Find_E_Parent();
        FindCam();
    }
	
	// Update is called once per frame
	void Update () {

        FindPlayer();

        if (transform.parent != null)
        {
            if (transform.parent.Equals(E_check.transform)
          && SIngleTonData.instance.ShotCheck == true)
            {
                if(DelayCheckOk == false)
                {
                    SIngleTonData.instance.SniperCheck = true;
                    CreateBullet();
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
            CopyPos = new Vector3(BasicVec.x + ForwardVector.x, BasicVec.y + ForwardVector.y + 0.8f, BasicVec.z + ForwardVector.z);
            Quaternion chekcRot = Player.transform.rotation;
            GameObject Bullet = (GameObject)Instantiate(SniperBulletObject, CopyPos, chekcRot);
            //카메라가 보는 시점으로 총알이 날라가겠끔
            ray = cam.ScreenPointToRay(Input.mousePosition);
            Bullet.transform.LookAt(ray.GetPoint(10.0f));
        }
    }
}
