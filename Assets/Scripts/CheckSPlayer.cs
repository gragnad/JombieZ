using UnityEngine;
using System.Collections;

public class CheckSPlayer : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
       /* if(col.CompareTag("Player"))
        {
            ZombieEnumy check = transform.parent.GetComponent<ZombieEnumy>();
            check.LookPlayer(col.gameObject);
        }*/
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {   
            ZombieEnumy check = transform.parent.GetComponent<ZombieEnumy>();
            check.LookPlayer(col.gameObject);
        }
    }

    //
   void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            ZombieEnumy check = transform.parent.GetComponent<ZombieEnumy>();
            check.BakcIdle();
        }
    }
}
