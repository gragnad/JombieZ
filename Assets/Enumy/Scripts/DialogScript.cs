using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogScript : MonoBehaviour {
    bool actived;

	// Use this for initialization
	void Start () {
        actived = false;    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchFunc()
    {
        if(!actived)
        {
            actived = true;
        }
        else
        {
            actived = false;
        }
        transform.FindChild("Image").GetComponent<Image>().enabled = actived;
        transform.FindChild("Text").GetComponent<Text>().enabled = actived;
    }

    public void setText(string str)
    {
        transform.FindChild("Text").GetComponent<Text>().text = str;
    }
}
