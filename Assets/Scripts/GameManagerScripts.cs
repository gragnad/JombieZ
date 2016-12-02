using UnityEngine;
using System.Collections;

public class GameManagerScripts : MonoBehaviour {

    public Camera MainCAm;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CameraChange();
    }

    void CameraChange()
    {
        CameraScripts CursorCheck = MainCAm.GetComponent<CameraScripts>();
        if (SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber] != null)
        {
            ItemScripts GunNumCheck = SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber].GetComponent<ItemScripts>();
            if (Input.GetMouseButtonDown(1) && GunNumCheck.ItemNumber == 2)
            {
                SIngleTonData.instance.camNumber = 1;
            }
            if (Input.GetMouseButtonUp(1) && GunNumCheck.ItemNumber == 2)
            {
                SIngleTonData.instance.camNumber = 0;
            }
        }    
        if(Input.GetKeyUp(KeyCode.Escape) && SIngleTonData.instance.camNumber != 2)
        {
            //총알 초기화
            for (int i = 0; i < 30; i++)
            {
                SIngleTonData.instance.g_BulletData[i].GetComponent<GunBulletSCripts>().BulletStart = false;
            }
            SIngleTonData.instance.camNumber = 2;
            CursorCheck.m_cursorIsLocked = false;

        }
        else if (Input.GetKeyUp(KeyCode.Escape) && SIngleTonData.instance.camNumber == 2)
        {
            
            SIngleTonData.instance.camNumber = 0;          
            CursorCheck.m_cursorIsLocked = true;
        }
    }
}
