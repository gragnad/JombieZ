using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UI_MiniMap_Scripts : MonoBehaviour {


    GameObject[] LocalMinimapEnumy;
    GameObject[] LocalMinimapItem;


    //player 의 기준치 가 0이된다!!
    public GameObject Player = null;
    Vector3 PlayerPos;

    Vector3[] EnumyCacaulationPos;
    Vector3[] itemCaculationPos;


    Image[] EnumyImagyIns;

    public Image ParentImage;
    public Image Dirction;

    public Image enumyImage;
    public Image PlayerImage;

	// Use this for initialization
	void Start () {
       
        PlayerPos = Vector3.zero;

        LocalMinimapEnumy = new GameObject[20];
        EnumyCacaulationPos = new Vector3[20];


        LocalMinimapItem = new GameObject[20];
        itemCaculationPos = new Vector3[20];

        EnumyImagyIns = new Image[20];
     

        for (int i = 0;i<20;i++)
        {
            EnumyImagyIns[i] = (Image)Instantiate(enumyImage,new Vector3(-2000,-2000,0),Quaternion.identity);
            EnumyImagyIns[i].transform.SetParent(ParentImage.transform);           
        }




    }
	
	// Update is called once per frame
	void Update () {

        PlayerPosiotionUpdate();
        CopyObject();
        BasicParentImage();

    }

    void BasicParentImage()
    {
        Quaternion ParentImageRot = Dirction.transform.rotation;


        Quaternion PlayerRot = Player.transform.rotation;

        //체크체크...
        //나중에 생각
     
        ParentImageRot.z =- PlayerRot.y;


        Dirction.transform.rotation = ParentImageRot;
    }


    void CopyObject()
    {
        for(int i = 0;i<20;i++)
        {
            if(SIngleTonData.instance.g_MiniMapEnumy[i] != null)
            {
                LocalMinimapEnumy[i] = SIngleTonData.instance.g_MiniMapEnumy[i];
                EnumyCacaulationPos[i] = LocalMinimapEnumy[i].transform.position - PlayerPos;
                Vector3 PosPasssOver = new Vector3(EnumyCacaulationPos[i].x, EnumyCacaulationPos[i].z, 0);
                EnumyImagyIns[i].transform.localPosition = PosPasssOver * 2f;
            }
            
            else if(SIngleTonData.instance.g_MiniMapEnumy[i] == null)
            {   
                EnumyImagyIns[i].transform.localPosition = new Vector3(-2000, -2000, 0);
            }  
        }
    }



    void PlayerPosiotionUpdate()
    {
        PlayerPos = Player.transform.position;
    }

}
