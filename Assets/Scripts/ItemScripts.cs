using UnityEngine;
using System.Collections;

public class ItemScripts : MonoBehaviour {

    public float GunDemeged;


    public int ItemNumber;
    public int PosNumber;
    public int BulletCount;

    public int BulletPlus;

    protected Camera cam = null;
    protected Vector3 CopyPos;
    protected Ray ray;
    protected GameObject Player = null;
    protected GameObject E_check = null;

    protected virtual void CreateBullet()
    {

    }

    protected void FindCam()
    {
        if (cam == null)
        {   
            Camera[] camcheck = FindObjectsOfType<Camera>();



            int k = camcheck.Length;
            for (int i = 0; i <k; i++)
            {
                if (camcheck[i].tag == "MainCamera")
                {
                    cam = camcheck[i];
                }
            }
        }
    }

    protected void FindPlayer()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    protected void Find_E_Parent()
    {
        E_check = GameObject.FindGameObjectWithTag("Check_Eitem");
    }


    //여기에 지정하면 출력 불렀을떄 다같이 먹게 된다...상속 받은 것들은
    protected void ReloadBullt(int itemBulletCount)
    {
        SIngleTonData.instance.Reload = true;
        StartCoroutine(RESetBullet(itemBulletCount));
    }

    protected IEnumerator RESetBullet(int BulletPlusLocal)
    {

        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 30; i++)
        {
            SIngleTonData.instance.g_BulletData[i].GetComponent<GunBulletSCripts>().BulletStart = false;
        }
        BulletPlus = BulletPlusLocal;
        SIngleTonData.instance.Reload = false;
    }
}
