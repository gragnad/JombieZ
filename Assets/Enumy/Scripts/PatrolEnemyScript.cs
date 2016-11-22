using UnityEngine;
using System.Collections;

public class PatrolEnemyScript : EnemyScript {
    public float moveDistance;
    bool playerHit = false;

    // Use this for initialization
    void Start () {
        enemyType = EnemyType.PATROL;
        state = EnemyState.PATROL;

        maxLife = 400;
        life = maxLife;

        moveSpeed = 2.0f;
        moveDistance = 3.0f;
        attackDelay = 1.0f;
        attacked = true;

        originPos = transform.position;
        originState = EnemyState.PATROL;

        animator = GetComponent<Animator>();

        timer = 0.0f;

   
    }
	
	// Update is called once per frame
	void Update () {
        
        if(animator.GetBool("DeadCheck") == false)
        {
            switch (state)
            {
                case EnemyState.PATROL:
                    
                    Patrol();
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
        else
        {
            Dead();
        }

    }

    void Patrol()
    {
        timer += Time.deltaTime;
        float temp = (transform.position - originPos).magnitude;
        if (temp < 0) temp *= -1;
        if (temp > moveDistance && timer > 1.5f)
        {
            timer = 0.0f;
            ChangeDir();
        }
        Move();
    }
}
