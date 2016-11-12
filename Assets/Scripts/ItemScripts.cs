using UnityEngine;
using System.Collections;

public class ItemScripts : MonoBehaviour {

    public int ItemNumber;
    public int PosNumber;
    public int BulletCount;
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
            for (int i = 0; i <= k; i++)
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
        E_check = GameObject.FindGameObjectWithTag("E_Item");
    }
}
