using UnityEngine;
using System.Collections;

public class GameManagerScripts : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        CameraChange();
    }

    void CameraChange()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SIngleTonData.instance.camNumber = 1;
        }
        if (Input.GetMouseButtonUp(1))
        {
            SIngleTonData.instance.camNumber = 0;
        }
        if(Input.GetKeyUp(KeyCode.Escape) && SIngleTonData.instance.camNumber != 2)
        {
            SIngleTonData.instance.camNumber = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && SIngleTonData.instance.camNumber == 2)
        {
            SIngleTonData.instance.camNumber = 0;
        }
    }
}
