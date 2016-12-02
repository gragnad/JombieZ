using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Player_Blood : MonoBehaviour {

    public Image UI_Blood_Player = null;

    Vector4 ChangeAlpha;

    float PlayerHpBarCopy;

    int PlusMinus_Player_Blood;

	// Use this for initialization
	void Start () {

        PlusMinus_Player_Blood = 0;
        UI_Blood_Player.enabled = false;
       ChangeAlpha = new Vector4(1, 1, 1, 1);
        UI_Blood_Player.color = ChangeAlpha;
        PlayerHpBarCopy = SIngleTonData.instance.HPBar;

    }
	
	// Update is called once per frame
	void Update () {
        CheckUIChange();

    }

    void CheckUIChange()
    {
        if(PlayerHpBarCopy > SIngleTonData.instance.HPBar)
        {
            UI_Blood_Player.enabled = true;
            ChangeAlphaSwitch();
        }
        else if(PlayerHpBarCopy == SIngleTonData.instance.HPBar)
        {
            UI_Blood_Player.enabled = false;
        }
    }

    void ChangeAlphaSwitch()
    {
        switch(PlusMinus_Player_Blood)
        {
            case 0 :
                BasicAlphaMinus();
                break;
            case 1:
                BasicAlphaPlus();
                break;
        }
    }

    void BasicAlphaMinus()
    {
        ChangeAlpha.w -= 0.1f;
        ChangeAlpha.x -= 0.1f;
        ChangeAlpha.y -= 0.1f;
        ChangeAlpha.z -= 0.1f;
        UI_Blood_Player.color = ChangeAlpha;
        if (ChangeAlpha.w <= 0)
        {
            PlusMinus_Player_Blood = 1;
        }


    }

    void BasicAlphaPlus()
    {
        ChangeAlpha.w += 0.1f;
        ChangeAlpha.x += 0.1f;
        ChangeAlpha.y += 0.1f;
        ChangeAlpha.z += 0.1f;
        UI_Blood_Player.color = ChangeAlpha;
        if (ChangeAlpha.w >= 1)
        {
            PlayerHpBarCopy = SIngleTonData.instance.HPBar;
            PlusMinus_Player_Blood = 0;

        }
    }
}
