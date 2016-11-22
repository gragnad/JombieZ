using UnityEngine;
using System.Collections;

public class ShootEnemyScript : EnemyScript {
    public GameObject bullet;

   
    // Use this for initialization
    void Start () {
        enemyType = EnemyType.SHOOT;
        state = EnemyState.IDLE;

        maxLife = 100;
        life = maxLife;

        moveSpeed = 2.0f;
        attackDelay = 3.0f;
        attacked = true;

        originPos = transform.position;
        originState = EnemyState.IDLE;

        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");

        timer = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {     
       if(animator.GetBool("DeadCheck") == false)
        {
            switch (state)
            {
                case EnemyState.IDLE:
                    animator.SetTrigger("Idle");
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

    IEnumerator ShootAttackCor()
    {
        yield return new WaitForSeconds(1.3f);
        GameObject obj = (GameObject)Instantiate(bullet, transform.position + new Vector3(0, 0.5f, 0.0f), transform.rotation);
        
        yield return new WaitForSeconds(1.4f);
        state = EnemyState.CHASE;

        yield return new WaitForSeconds(attackDelay);
        attacked = true;

    }


}
