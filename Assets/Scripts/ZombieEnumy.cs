using UnityEngine;
using System.Collections;

public class ZombieEnumy : MonoBehaviour
{


    Animator anim = null;

    //Hp
    public float Enumy_HP;

    public bool AttackStart = false;

    //
    public GameObject BloodEffect;
    //
    bool Dead_Check_stop;
        //
    GameObject PlayerCopy;
    //
    GameObject EnumyAttackPosition;


    BoxCollider checkAttack;
    protected int enumyState = 0;

    //ParticleSystem PartSystem;
   // GameObject PartcleObject;// = new GameObject[5];

    enum enumyStetecheck
    {
        idle,
        move,
        Attack,
        dead
    }


    // Use this for initialization
    void Start()
    {

        Dead_Check_stop = false;
        AttackStart = false;

        Enumy_HP = 40.0f;

        BloodEffect = GameObject.FindGameObjectWithTag("Blood");
        checkAttack = transform.FindChild("Attack_Check").GetComponent<BoxCollider>();
        enumyState = (int)enumyStetecheck.idle;
        anim = GetComponent<Animator>();
       
      
    }

    // Update is called once per frame
    void Update()
    {

        if (AttackStart == true)
        {
            enumyState = (int)enumyStetecheck.Attack;
        }

        if (Dead_Check_stop == false)
        {
            state();

          
            DeaD();
        }
    }

    public void LookPlayer(GameObject Player)
    {
        if (Player != null)
        {
            PlayerCopy = Player;
            transform.LookAt(Player.transform);
            enumyState = (int)enumyStetecheck.move;
        }
    }


    public void BakcIdle()
    {
        enumyState = (int)enumyStetecheck.idle;
    }



    protected void state()
    {
        switch (enumyState)
        {
            case (int)enumyStetecheck.idle:
                anim.SetBool("walk", false);
                break;

            case (int)enumyStetecheck.move:
                anim.SetBool("walk", true);
                transform.position += transform.forward * 1.0f * DeltaTImeData.instance.DeltaTime;
                break;

            case (int)enumyStetecheck.Attack:
                if (AttackStart == true && PlayerCopy != null)
                {
                    anim.SetBool("Attack", true);
                    SIngleTonData.instance.HPBar -= 0.2f;
                    AttackStart = false;
                    StartCoroutine(AttackDelay());
                }
                break;
            case (int)enumyStetecheck.dead:
                if(Dead_Check_stop == false)
                {
                    anim.SetBool("Dead", true);
                    StartCoroutine(DeadDelay());
                    Dead_Check_stop = true;
                }             
                break;
        }
    }

   /* void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Bullet"))
        {
            ItemScripts GunNumberDegedCheck = SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber].GetComponent<ItemScripts>();
            Enumy_HP -= GunNumberDegedCheck.GunDemeged;
        }
    }*/

    /*void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
            ItemScripts GunNumberDegedCheck = SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber].GetComponent<ItemScripts>();
            Enumy_HP -= GunNumberDegedCheck.GunDemeged;
        }
    }*/
    
    void DeaD()
    {
        if(Enumy_HP <= 0)
        {
            enumyState = (int)enumyStetecheck.dead;
            transform.FindChild("CheckAround").GetComponent<BoxCollider>().enabled = false;
        }
    }   
   IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.1f);   
        Vector3 EffectPos = PlayerCopy.transform.position;
        EffectPos.y += 1.5f;
        GameObject CopyBloodParticle = (GameObject)Instantiate(BloodEffect, EffectPos, transform.rotation);    
        SIngleTonData.instance.PlayerStop = true;
        checkAttack.enabled = false;      
       
        yield return new WaitForSeconds(1.0f);
        Destroy(CopyBloodParticle);
        anim.SetBool("Attack", false);
        SIngleTonData.instance.PlayerStop = false;

        yield return new WaitForSeconds(1.5f);
        checkAttack.enabled = true;
        BakcIdle();
    }

    IEnumerator DeadDelay()
    {
        yield return new WaitForSeconds(4.5f);

        Destroy(gameObject);
    }
}
