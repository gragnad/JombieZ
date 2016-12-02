using UnityEngine;
using System.Collections;

public class G_AuidioData : MonoBehaviour {




    private static G_AuidioData AuidioData = null;
    public static G_AuidioData auidioData
    {
        get
        {
            if (AuidioData == null)
            {
                AuidioData = new G_AuidioData();
            }
            return AuidioData;
        }
    }
    private G_AuidioData()
    {

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
