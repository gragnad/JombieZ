using UnityEngine;
using System.Collections;

public class GunBulletSCripts : MonoBehaviour {

    Vector3 EndBulletPos;
    Vector3 EndBulletPosMinus;

    // Use this for initialization
    void Start () {
        EndBulletPos= new Vector3(30,30,30);
        EndBulletPosMinus = new Vector3(-30, -30, -30);
                 
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
}
