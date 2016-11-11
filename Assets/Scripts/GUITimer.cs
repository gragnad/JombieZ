using UnityEngine;
using System.Collections;

public class GUITimer : MonoBehaviour {

    float timer;
    float fps;
    public float Second = 30f;
    public int Min = 3;
	// Use this for initialization
	void Start () {
        Second = 30;

    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //Debug.Log("timer : "+timer/fps+" seconds");
	}

    void OnGUI()
    {
        int w = Screen.width;
        int h = Screen.height;

        GUIStyle style = new GUIStyle();

        GUIStyle style2 = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 25);
        style.alignment = TextAnchor.UpperCenter;
        style.fontSize = h * 2 / 25;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        style.fontStyle = FontStyle.BoldAndItalic;


        style2.alignment = TextAnchor.UpperCenter;
        style2.fontSize = h * 2 / 24;
        style2.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        style2.fontStyle = FontStyle.BoldAndItalic;

        if(SIngleTonData.instance.camNumber != 2)
        {
            Second -= 0.5f * Time.deltaTime;
            if (Second <= 0)
            {
                Min--;
                Second = 60;
            }

            fps = 1.0f / Time.deltaTime;
                  
        }
        string text = string.Format("{0:0} : {1:0.00}", Min, Second);
        GUI.Label(rect, text, style2);
        GUI.Label(rect, text, style);
    }

    void BasicTimeCheckUpdate()
    {
        SIngleTonData.instance.Min = Min;
        SIngleTonData.instance.SeCond = Second;
    }
}
