using UnityEngine;
using System.Collections;

public class IetmMoving : MonoBehaviour {

    float RotY;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LocalRotationUPdate();

    }

    void LocalRotationUPdate()
    {
        if(transform.parent == null)
        {
            float RotYPlus= Time.deltaTime * 30.0f;
            RotY += RotYPlus;          
            if (RotY >= 360)
            {
                RotY = 0;
                transform.rotation = Quaternion.identity;
            }
            Vector3 CRotate = new Vector3(0, RotY, 0);
            transform.rotation = Quaternion.Euler(CRotate); 
        }
    }
}
