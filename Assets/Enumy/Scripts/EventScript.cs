using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class EventScript : MonoBehaviour {
    public enum State
    {
        TUTORIAL,
    };

    public State state;
    GameObject obj;
    string fileName;
    bool lived;
    
	// Use this for initialization
	void Start () {
        if(state == State.TUTORIAL)
        {
            fileName = "tutorial.txt";
        }
        lived = true;
        obj = GameObject.Find("Canvas").transform.FindChild("EventDialog").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            if(lived)
            {
                obj.GetComponent<DialogScript>().SwitchFunc();
                StartCoroutine("ReadData");
                lived = false;
            }
        }
    }

    IEnumerator ReadData()
    {
        
        StreamReader sr = File.OpenText(Application.dataPath + "/" + fileName);
       

        string str = sr.ReadLine();

        while (str != null)
        {

            obj.GetComponent<DialogScript>().setText(str);
            yield return new WaitForSeconds(3.0f);
            str = sr.ReadLine();
        }
        obj.GetComponent<DialogScript>().SwitchFunc();
        sr.Close();
    }
}
