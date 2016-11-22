using UnityEngine;
using System.Collections;

public class GuardEnemyScript : EnemyScript {

   
    // Use this for initialization
    void Start () {
//        DefineVal();
        enemyType = EnemyType.GUARD;
        state = EnemyState.IDLE;

        timer = 0.0f;
        maxLife = 400;
        life = maxLife;
        moveSpeed = 4.0f;
        attackDelay = 1.0f;

        originPos = transform.position;
        originState = EnemyState.IDLE;
        animator = GetComponent<Animator>();
        attacked = true;
       
    }
	
	// Update is called once per frame
	void Update () {
        if (animator.GetBool("DeadCheck") == false)
        {
            switch (state)
            {
                case EnemyState.IDLE:
                   
                  
                    break;
                case EnemyState.CHASE:
                    Chase();
                  
                    break;
                case EnemyState.ATTACK:

                    //                Attack();
                    break;
                case EnemyState.BACK:
                    Back();
                    break;
                case EnemyState.DEAD:
                    Dead();
                    break;
            }
        }
    }

}
