using UnityEngine;
using System.Collections;

public class SaveBulletScirpts : MonoBehaviour {

    public GameObject Gun_Bullet= null;

    Vector3 BasicVEctor = new Vector3(0, -20, 0);
    Quaternion BasicQuaternion = Quaternion.identity;

    GameObject[] BulletSaves = new GameObject[30];
	// Use this for initialization
	void Start () {

        //기본 30발 저장
	    for(int i = 0;i<30;i++)
        {
            BulletSaves[i] = (GameObject)Instantiate(Gun_Bullet, BasicVEctor, BasicQuaternion);
            SIngleTonData.instance.g_BulletData[i] = BulletSaves[i];
        }
	}
}
