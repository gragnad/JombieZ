using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class REloadScripts : MonoBehaviour {

    public Image Reload = null;

    int PlusMinus = 0;
    Vector4 CheckAlpha;

    // Use this for initialization
    void Start () {
        Reload.enabled = false;
        CheckAlpha = new Vector4(1, 1, 1,1);

    }
	
	// Update is called once per frame
	void Update () {
        if(SIngleTonData.instance.Reload == true)
        {
            BasicAlphaChange();
        }       
        CheckReload();
    }

    void BasicAlphaChange()
    {
        switch(PlusMinus)
        {
            case 0:
                BasicAlphaMinus();
                break;
            case 1:
                BasicAlphaPlus();
                break;
        }    
    }

    void BasicAlphaMinus()
    {
        CheckAlpha.w -= 0.1f;
        CheckAlpha.x -= 0.1f;
        CheckAlpha.y -= 0.1f;
        CheckAlpha.z -= 0.1f;
        Reload.color = CheckAlpha;
        if (CheckAlpha.w <= 0)
        {
            PlusMinus = 1;
        }


    }

    void BasicAlphaPlus()
    {
        CheckAlpha.w += 0.1f;
        CheckAlpha.x += 0.1f;
        CheckAlpha.y += 0.1f;
        CheckAlpha.z += 0.1f;
        Reload.color = CheckAlpha;
        if (CheckAlpha.w >= 1)
        {
            PlusMinus = 0;
        }
    }

    void CheckReload()
    {
        if(SIngleTonData.instance.Reload == true)
        {
            Reload.enabled = true;
        }
        else if(SIngleTonData.instance.Reload == false)
        {
            Reload.enabled = false;
        }
    }
}
