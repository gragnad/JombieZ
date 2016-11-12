using UnityEngine;
using System.Collections;


public class CharicterCon : MonoBehaviour
{

    //이동만 넣고
    public GameObject Player = null;
    //

    //
    GameObject[] localInvenTory = new GameObject[4];
    int InvenToryNum = 0;
    //
    bool ItemEatCheck;
    //


    GameObject[] EquirMentItem = new GameObject[2];
    //
    

    //UI 이용 장착이니 싱글턴 으로
    //GameObject[] Inventory = new GameObject[4];
    //
    GameObject EqiirMentItemPosition;
    // Use this for initialization
    void Start()
    {
        DeltaTImeData.instance.DelataTimeCheck = true;
        DeltaTImeData.instance.TimeClassDeltaTime();

        ItemEatCheck = false;

       

        EqiirMentItemPosition = GameObject.FindGameObjectWithTag("E_Item");
    }
    
    // Update is called once per frame
    void Update()
    {

        ChangeButtonClickItem();

        if (SIngleTonData.instance.UiChangeItem == false)
        {
            if (localInvenTory[InvenToryNum] != null)
            {
                EquirMentItem[0] = localInvenTory[InvenToryNum];
            }
            if (Input.GetKeyUp(KeyCode.U))
            {   
                InvenToryNum++;
               
                if (InvenToryNum >= 4)
                {
                    InvenToryNum = 0;
                }

            }
            //shot 에 관한 것들
            if(SIngleTonData.instance.camNumber != 2)
            {
                ShotBullet();
            }        

            BasicPlayerMove();

            setEquirMent();

            SaveItem();

            CopyItemForUI();

            DropItem();          
        }  
    }

    private void ShotBullet()
    {
        if (localInvenTory[InvenToryNum] != null)
        {
            ItemScripts scriptsCheck = localInvenTory[InvenToryNum].GetComponent<ItemScripts>();

            if (Input.GetMouseButtonDown(0) && scriptsCheck.BulletCount > 0)
            {
                SIngleTonData.instance.ShotCheck = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                SIngleTonData.instance.ShotCheck = false;
            }
        }
    }

    private void ChangeButtonClickItem()
    {
        if (SIngleTonData.instance.UiChangeItem == true)
        {
            InvenToryNum = SIngleTonData.instance.InvenToryNumber;
            if (localInvenTory[InvenToryNum] != null)
            {
                localInvenTory[InvenToryNum].transform.GetComponent<BoxCollider>().enabled = false;
            }
            SIngleTonData.instance.UiChangeItem = false;
        }
    }

    //ui 에 따른 카피해서 그림을 띄워 준다 button에...
    void CopyItemForUI()
    {
        if(localInvenTory[InvenToryNum] != null)
        {
            SIngleTonData.instance.Inventory[InvenToryNum] = localInvenTory[InvenToryNum];
            SIngleTonData.instance.InvenToryNumber = InvenToryNum;           
        }
        else if(localInvenTory[InvenToryNum] == null)
        {
            SIngleTonData.instance.Inventory[InvenToryNum] = null;
            SIngleTonData.instance.InvenToryNumber = InvenToryNum;
        }
    }

    void SaveItem()
    {

        for (int i = 0; i < 4; i++)
        {
            if (i != InvenToryNum && localInvenTory[i] != null)
            {               
                //등쪽으로 가기
                localInvenTory[i].transform.SetParent(Player.transform);
                localInvenTory[i].transform.localPosition = new Vector3(0, 0.8f, -0.2f);
                localInvenTory[i].transform.rotation = Quaternion.Euler(90, -Player.transform.rotation.y, Player.transform.rotation.z);
            }
            else if (i == InvenToryNum && localInvenTory[InvenToryNum] != null)
            {
                localInvenTory[InvenToryNum].transform.SetParent(EqiirMentItemPosition.transform);
                localInvenTory[InvenToryNum].transform.localPosition = Vector3.zero;
                localInvenTory[InvenToryNum].transform.rotation = Player.transform.rotation;
            }
        }     
    }

    void BasicPlayerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Player.transform.position += Player.transform.forward * 2.0f * DeltaTImeData.instance.DeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Player.transform.position += Player.transform.forward * -2.0f * DeltaTImeData.instance.DeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.position += Player.transform.right * 2.0f * DeltaTImeData.instance.DeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Player.transform.position += Player.transform.right * -2.0f * DeltaTImeData.instance.DeltaTime;
        }
       
    }

    //아이템 장착에 관한...
    void setEquirMent()
    {
        if (Input.GetMouseButtonUp(0) == true && ItemEatCheck == true)
        {
            if (EquirMentItem[1] != null)
            {
                EquirMentItem[0].transform.GetComponent<BoxCollider>().enabled = true;
                EquirMentItem[0].transform.parent = null;
                EquirMentItem[0] = EquirMentItem[1];
                localInvenTory[InvenToryNum] = EquirMentItem[0];           
                EquirMentItem[1] = null;
                
            }
            //스크립트로도 가져와서 아이템 넘버 받은것에 대한 효과
            else if(EquirMentItem[0] != null)
            {                
                EquirMentItem[0].transform.GetComponent<BoxCollider>().enabled = false;
                localInvenTory[InvenToryNum] = EquirMentItem[0];
                ItemEatCheck = false;
            }
            else if(EquirMentItem[0] == null)
            {
                localInvenTory[InvenToryNum] = null;
            }
            

        }
    }

    void DropItem()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            if(localInvenTory[InvenToryNum] != null)
            {
                localInvenTory[InvenToryNum].transform.parent = null;                              
                localInvenTory[InvenToryNum].transform.GetComponent<BoxCollider>().enabled = true;
                localInvenTory[InvenToryNum] = null;
                EquirMentItem[InvenToryNum] = null;
                SIngleTonData.instance.Inventory[InvenToryNum] = localInvenTory[InvenToryNum];
                //StartCoroutine(DropReturnfalse());
            }                     
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Item")
        {
            if(EquirMentItem[0] == null)
            {
                EquirMentItem[0] = col.gameObject;
            }           
            else if(EquirMentItem[0] != null && EquirMentItem[0] != EquirMentItem[1])
            {
                EquirMentItem[1] = col.gameObject;
            }
            ItemEatCheck = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Item")
        {
            EquirMentItem[0] = null;
            ItemEatCheck = false;
        }
    }

}
