using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Seee_Bullet_Count : MonoBehaviour {

    public Text Bullet_Count_Text;

	// Use this for initialization
	void Start () {
        Bullet_Count_Text.text = " ";

    }
	
	// Update is called once per frame
	void Update () {
        CheckBullet();

    }

    void CheckBullet()
    {
        if(SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber] != null)
        {
            if(SIngleTonData.instance.Reload == false)
            {
                ItemScripts Check_Bullet_Count_See = SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber].GetComponent<ItemScripts>();
                Bullet_Count_Text.text = string.Format("{0}", Check_Bullet_Count_See.BulletPlus);
            }         
        }
       
    }
}
