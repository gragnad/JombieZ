using UnityEngine;
using System.Collections;

public class HideAreaScript : MonoBehaviour {
    public GameObject hideEnemy;
    //4마리 출현  위치 랜덤 ?
    Vector3 pos;

    // Use this for initialization
    void Start () {
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {

            Vector3 pos = col.transform.position;

            for (int i = 0; i < 2; ++i)
            {


                GameObject obj = (GameObject)Instantiate(hideEnemy, pos + new Vector3(-4 * (i + 1), 0, 0), transform.rotation);
            }
            for (int i = 0; i < 2; ++i)
            {

                GameObject obj = (GameObject)Instantiate(hideEnemy, pos + new Vector3(4 * (i + 1), 0, 0), transform.rotation);
            }
            GetComponent<BoxCollider>().enabled = false;
        }
    }

}
