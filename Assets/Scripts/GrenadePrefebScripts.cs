using UnityEngine;
using System.Collections;

public class GrenadePrefebScripts : MonoBehaviour
{

    public Rigidbody rigid = null;

    public GameObject BombParticle = null;
    float DownCheck = 20.0f;

    bool EffectCopyCheck = false;
   
    // Use this for initialization
    void Start()
    {
        rigid.useGravity = false;
        BombParticle = GameObject.FindGameObjectWithTag("Fire");
    }

    // Update is called once per frame
    void Update()
    {

        GrenadeBasic();

    }

    void GrenadeBasic()
    {
        transform.position += transform.forward * Time.deltaTime * DownCheck;
        if (DownCheck > 5)
        {
            DownCheck--;

        }
        else if (DownCheck <= 5)
        {
            rigid.useGravity = true;
            
        }
        StartCoroutine(DownForce());

    }

    IEnumerator DownForce()
    {
        yield return new WaitForSeconds(1.0f);

      
        StartCoroutine(BombCheck());
    }

    IEnumerator BombCheck()
    {
        yield return new WaitForSeconds(1.0f);

        if (EffectCopyCheck == false)
        {
           
            GameObject Particle = (GameObject)Instantiate(BombParticle, transform.position, transform.rotation);
            EffectCopyCheck = true;

            yield return new WaitForSeconds(0.5f);
          
            Destroy(Particle);
            Destroy(this.gameObject);
        }
    }
}
   

 