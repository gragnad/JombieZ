using UnityEngine;
using System.Collections;

public class EnemyAtkChk : MonoBehaviour {
    // Use this for initialization

   
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
       
	}

   

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {            
            transform.parent.GetComponent<EnemyScript>().Attack();   
                                   
        }            
    }

   
}
