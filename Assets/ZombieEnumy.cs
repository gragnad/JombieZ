using UnityEngine;
using System.Collections;

public class ZombieEnumy : MonoBehaviour
{


    Animator anim = null;

    public bool AttackStart = false;

    protected int enumyState = 0;

    //ParticleSystem PartSystem;
   // GameObject PartcleObject;// = new GameObject[5];

    enum enumyStetecheck
    {
        idle,
        move
    }


    // Use this for initialization
    void Start()
    {
        enumyState = (int)enumyStetecheck.idle;
        anim = GetComponent<Animator>();
       
      
    }

    // Update is called once per frame
    void Update()
    {
        state();
    }

    public void LookPlayer(GameObject Player)
    {
        if (Player != null)
        {
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
        }
    }
   
   

}
