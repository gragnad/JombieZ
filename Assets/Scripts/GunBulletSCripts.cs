using UnityEngine;
using System.Collections;

public class GunBulletSCripts : MonoBehaviour {

    Vector3 BasicVec = new Vector3(0, -20, 0);
    GameObject B_Particle;
    public  bool BulletStart = false;
    bool EffectCopyCheck = false;
    public TrailRenderer Trail_Render = null;
    //
    bool Trail_Render_Check;
    // Use this for initialization
    void Start () {

        Trail_Render_Check = true;
        B_Particle = GameObject.FindGameObjectWithTag("Blood");          
    }
	
	// Update is called once per frame
	void Update () {
            
        BulletBasic();
    }

    void BulletBasic()
    {         
        if(BulletStart == true )
        {
            Trail_Render.enabled = true;
            transform.position += transform.forward * DeltaTImeData.instance.DeltaTime * 10.0f;
           /* if(Trail_Render_Check == true)
            {
                Trail_Render_Check = false;
                StartCoroutine(OffTrail_Render());
            }   */       
        }
        else if(BulletStart == false)
        {
            transform.position = BasicVec;
            Trail_Render.enabled = false;
        }       
    }

   

    /*void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Enumy"))
        {          
            StartCoroutine(BloodPartcle());
        }       
    }*/




    void OnTriggerEnter(Collider col)
    {   
        if (col.CompareTag("Enumy"))
        {   

            StartCoroutine(BloodPartcle());
            ItemScripts GunNumberDegedCheck = SIngleTonData.instance.Inventory[SIngleTonData.instance.InvenToryNumber].GetComponent<ItemScripts>();
            ZombieEnumy CheckDemged = col.transform.GetComponent<ZombieEnumy>();
            CheckDemged.Enumy_HP -= GunNumberDegedCheck.GunDemeged;
        }
        else if (col.CompareTag("ComBackBullet"))
        {
            BulletStart = false;
        }
    }
    //트레일 렌더 시작후 바로 꺼지게
    IEnumerator OffTrail_Render()
    {
        yield return new WaitForSeconds(0.5f);
     
        Trail_Render.enabled = false;
    }



    //파티클은 가져와야 하니
    IEnumerator BloodPartcle()
    {
        yield return new WaitForSeconds(0.1f);

        if(EffectCopyCheck == false)
        {
            GameObject BloodCreate = (GameObject)Instantiate(B_Particle, transform.position, transform.rotation);
            EffectCopyCheck = true;

            
           

            yield return new WaitForSeconds(0.5f);

            BulletStart = false;
            Destroy(BloodCreate);
            EffectCopyCheck = false;
        }
       
    }
}
