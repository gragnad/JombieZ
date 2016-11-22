using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public enum EnemyType
    {
        PATROL,
        GUARD,
        HIDE,
        SHOOT,
    };

    public enum EnemyState
    {
        //기본
        IDLE,
        PATROL,
        //경계
        BACK,
        ALERT,
        //적대
        CHASE,
        AVOID,
        ATTACK,
        DEAD,
    };

    protected Animator animator;
    protected GameObject target = null;
    protected GameObject aggroSprite;
    protected Vector3 originPos;
    protected EnemyState originState;

    protected EnemyType enemyType;
    protected EnemyState state;

    protected int life;
    protected int maxLife;    
    protected float moveSpeed;
    protected float timer;
    protected float attackDelay;
    protected bool attacked;

   

    protected void Move()
    {
        animator.SetBool("walk",true);
        transform.position += (transform.forward * moveSpeed * DeltaTImeData.instance.DeltaTime);
       
    }

    protected void ChangeDir()
    {
        transform.Rotate(new Vector3(0, 180, 0));
    }

    protected void LookDir(Vector3 pos)
    {
        Vector3 tempDir = Vector3.Normalize(pos - transform.position);

        tempDir.y = 0;
        transform.rotation = Quaternion.LookRotation(tempDir);
    }

    public void Attack()
    {
       
        if (attacked && state != EnemyState.ATTACK)
        {
            attacked = false;
            state = EnemyState.ATTACK;
           
            if(enemyType == EnemyType.SHOOT)
            {
                StartCoroutine("ShootAttackCor");
            }
            else
            {
                StartCoroutine(AttackDelayCheck());
            }
        }
    }

    protected void Dead()
    {
      
        if (state != EnemyState.DEAD)
        {
            state = EnemyState.DEAD;
           
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > 2.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void Chase()
    {
       
        if (target != null)
        {
            LookDir(target.transform.position);
        }

        Move();
    }

    public void Damaged(int damage)
    {
        if(target == null)
        {
            FindPlayer(GameObject.FindGameObjectWithTag("Player"));
        }

        life -= damage;
        if(life < 1)
        {
            Dead();
        }
    }


    protected void Back()
    {
        transform.LookAt(originPos);
       
        Move();
        if((originPos - transform.position).magnitude < 2.0f)
        {
            //초기 상태로
            state = originState;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void FindPlayer(GameObject obj)
    {
        target = obj;
        state = EnemyState.CHASE;
        animator.SetBool("walk",true);
    }

    public void LostPlayer()
    {
        target = null;
        if (state != EnemyState.DEAD)
        {            
            state = EnemyState.BACK;
            animator.SetBool("walk", true);
        }
    }
    public EnemyType getType()
    {
        return enemyType;
    }

    public EnemyState getState()
    {
        return state;
    }
    public void setState(EnemyState s)
    {
        state = s;
    }

    IEnumerator AttackDelayCheck()
    {
        yield return new WaitForSeconds(1.0f);
        transform.FindChild("AttackRange").GetComponent<BoxCollider>().enabled = true;

        yield return new WaitForSeconds(0.7f);
        transform.FindChild("AttackRange").GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(1.0f);
        state = EnemyState.CHASE;
       

        yield return new WaitForSeconds(attackDelay);
        attacked = true;
        
    }

    
}
