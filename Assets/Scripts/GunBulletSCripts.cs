using UnityEngine;
using System.Collections;

public class GunBulletSCripts : MonoBehaviour {

    Vector3 EndBulletPos;
    Vector3 EndBulletPosMinus;

   GameObject B_Particle;
    bool EffectCopyCheck = false;
    // Use this for initialization
    void Start () {
        Vector3 DesCheck = transform.position;
        EndBulletPos = new Vector3(DesCheck.x + 15, DesCheck.y + 15, DesCheck.z + 15);
        EndBulletPosMinus = new Vector3(DesCheck.x + -15, DesCheck.y + -15, DesCheck.z + -15);
        B_Particle = GameObject.FindGameObjectWithTag("Blood");          
    }
	
	// Update is called once per frame
	void Update () {
            
        BulletBasic();

        BulletDestory();

    }

    void BulletBasic()
    {            
         transform.position += transform.forward * DeltaTImeData.instance.DeltaTime * 5.0f;       
    }

    void BulletDestory()
    {
        Vector3 EndCheckPos= transform.position;
        if(EndBulletPos.x < EndCheckPos.x || EndBulletPos.y < EndCheckPos.y || EndBulletPos.z < EndCheckPos.z
            || EndBulletPosMinus.x > EndCheckPos.x || EndBulletPosMinus.y > EndCheckPos.y || EndBulletPosMinus.z > EndCheckPos.z)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Enumy"))
        {          
            StartCoroutine(BloodPartcle());
        }
    }

    IEnumerator BloodPartcle()
    {
        yield return new WaitForSeconds(0.1f);

        if(EffectCopyCheck == false)
        {
            GameObject BloodCreate = (GameObject)Instantiate(B_Particle, transform.position, transform.rotation);
            EffectCopyCheck = true;
            yield return new WaitForSeconds(0.8f);

            Destroy(BloodCreate);
            Destroy(gameObject);

        }
       
    }
}
