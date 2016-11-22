using UnityEngine;
using System.Collections;

public class HideEnemyScript : EnemyScript
{

   
    // Use this for initialization
    void Start () {
        enemyType = EnemyType.HIDE;

        maxLife = 200;
        life = maxLife;

        moveSpeed = 2.0f;

        attackDelay = 1.0f;
        attacked = true;

        originPos = transform.position;
        originState = EnemyState.CHASE;

        animator = GetComponent<Animator>();

        timer = 0.0f;

        FindPlayer(GameObject.FindGameObjectWithTag("Player"));
    }
	
	// Update is called once per frame
	void Update () {  
        if(animator.GetBool("DeadCheck") == false)
        {
            switch (state)
            {
                case EnemyState.CHASE:
                    Chase();
                    break;
                case EnemyState.ATTACK:

                    //                Attack();
                    break;
                case EnemyState.DEAD:
                    Dead();
                    break;
            }
        }
        else
        {
            Dead();
        }
    }

}
