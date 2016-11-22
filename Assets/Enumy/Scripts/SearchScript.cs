using UnityEngine;
using System.Collections;

public class SearchScript : MonoBehaviour {
    enum SearchState
    {
        NORMAL,
        FIND,
        LOST,
    };

    GameObject enemy = null;
    float timer;
    SearchState state;
	// Use this for initialization
	void Start () {
        enemy = transform.parent.gameObject;
        timer = 0.0f;
        state = SearchState.NORMAL;
	}
	
	// Update is called once per frame
	void Update () {
	    if(state == SearchState.LOST)
        {
            timer += Time.deltaTime;
            enemy.GetComponent<EnemyScript>().setState(EnemyScript.EnemyState.IDLE);

            if(timer > 3.0f)
            {
                timer = 0.0f;
                enemy.GetComponent<EnemyScript>().LostPlayer();
                state = SearchState.NORMAL;
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if(enemy.GetComponent<EnemyScript>().getState() == EnemyScript.EnemyState.DEAD)
        {
            return;
        }
        if(col.CompareTag("Player"))
        {
            timer = 0.0f;
            enemy.GetComponent<EnemyScript>().FindPlayer(col.gameObject);
            state = SearchState.FIND;
        }
       
    }

    void OnTriggerStay(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            state = SearchState.LOST;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            state = SearchState.LOST;
        }
    }

}
