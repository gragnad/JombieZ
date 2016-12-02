using UnityEngine;
using System.Collections;

public class MiniMap_Scripts : MonoBehaviour {


    public GameObject player = null;

    //싱글톤 으로 넘기자
    GameObject[] InerEnumyObject;

    int Enumycount;

    // Use this for initialization
    void Start () {

        InerEnumyObject = new GameObject[20];

        //널값으로 초기화
        for(int i = 0;i<20;i++)
        {
            InerEnumyObject[i] = null;
        }

        Enumycount = 0;
      
	    //플레이어는 어차피 중간
	}
	
	// Update is called once per frame
	void Update () {
        MinimapPosition();
        CopySingleton();
        FarDistance();
    }

    void MinimapPosition()
    {
        transform.position = player.transform.position;
    }


    void CopySingleton()
    {
        for(int i =0;i<20;i++)
        {
            //if(InerEnumyObject[i] != null)
            {
                SIngleTonData.instance.g_MiniMapEnumy[i] = InerEnumyObject[i];
            }
        }
    }

    void FarDistance()
    {
        for(int i = 0;i<20;i++)
        {
            if(InerEnumyObject[i] != null)
            {
                 float checkDisTance = Vector3.Distance(InerEnumyObject[i].transform.position, player.transform.position);
                if(checkDisTance >= 24)
                {
                    InerEnumyObject[i] = null;
                }
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        
        if (col.CompareTag("Enumy"))
        {
            if (InerEnumyObject[Enumycount] == null)
            {
                InerEnumyObject[Enumycount] = col.gameObject;
            }
            else if (InerEnumyObject[Enumycount] != null 
                && InerEnumyObject[Enumycount+1] != InerEnumyObject[Enumycount])
            {
                Enumycount++;
                InerEnumyObject[Enumycount] = col.gameObject;
                if (Enumycount >= 20)
                {
                    Enumycount = 0;
                }
                
            }         
        }
    }

   
}
