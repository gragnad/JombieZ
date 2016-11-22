using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HPBArScripts : MonoBehaviour {

    public Image HPBarImage = null;

    public Image HPBarBackCase = null;

    float ChangeHPbar;

    Vector3 ChangeHpBarVector;

    // Use this for initialization
    void Start () {

        SIngleTonData.instance.HPBar = HPBarImage.transform.localScale.x;
        HPBarBackCase.transform.localScale = HPBarImage.transform.localScale;

        HPBarImage.rectTransform.position = new Vector2(9, 690);
        HPBarBackCase.rectTransform.position = new Vector2(9, 690);

       
        ChangeHPbar = SIngleTonData.instance.HPBar;
    }
	
	// Update is called once per frame
	void Update () {
        HPCheck();

        HPbbarBackCaculation();

    }

    private void HPCheck()
    {
        HPBarImage.transform.localScale = new Vector3(SIngleTonData.instance.HPBar, 0.25f, 1);
        if (ChangeHPbar != SIngleTonData.instance.HPBar)
        {
            ChangeHPbar = SIngleTonData.instance.HPBar;
            HPBarImage.transform.localScale = new Vector3(ChangeHPbar, 0.25f, 1);
        }
    }



    private void HPbbarBackCaculation()
    {
        ChangeHpBarVector.x = HPBarBackCase.transform.localScale.x;
        ChangeHpBarVector.y = 0.25f;
        ChangeHpBarVector.z = 1f;

        if (HPBarImage.transform.localScale.x != HPBarBackCase.transform.localScale.x)
        {
            ChangeHpBarVector.x -= Time.deltaTime * 0.9f;

            HPBarBackCase.transform.localScale = ChangeHpBarVector;
            if (HPBarImage.transform.localScale.x >= HPBarBackCase.transform.localScale.x)
            {
                HPBarBackCase.transform.localScale = HPBarImage.transform.localScale;
            }
        }
    }

}
