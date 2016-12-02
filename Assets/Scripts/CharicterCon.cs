using UnityEngine;
using System.Collections;


public class CharicterCon : MonoBehaviour
{
  
    public float JumpPower = 4.5f;
    public Animator animation;
    public Rigidbody rigid;
    int JumpState = 0;
    int RunState = 0;
    int BackState = 0;
    int LeftState = 0;
    int RightState = 0;
    //
    int GunStatePoint = 0;
    //이동만 넣고
    public GameObject Player = null;
    //

    //
    GameObject[] localInvenTory = new GameObject[4];
    int InvenToryNum = 0;
    //
    bool ItemEatCheck;
    //
    GameObject ItemFolder = null;

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

        //EqiirMentItemPosition = GameObject.FindGameObjectWithTag("E_Item");
       EqiirMentItemPosition = GameObject.FindGameObjectWithTag("Check_Eitem");

       

        ItemFolder = GameObject.FindGameObjectWithTag("folder");
    }
    
    // Update is called once per frame
    void Update()
    {

        if (SIngleTonData.instance.PlayerStop == false)
        {
            /*if (Input.GetKeyUp(KeyCode.R))
            {
                SIngleTonData.instance.HPBar -= 0.5f;
                if (SIngleTonData.instance.HPBar <= 0)
                {
                    SIngleTonData.instance.HPBar = 4.0f;
                }
            }*/

            ChangeButtonClickItem();

            if (SIngleTonData.instance.UiChangeItem == false)
            {
                if (localInvenTory[InvenToryNum] != null)
                {
                    EquirMentItem[0] = localInvenTory[InvenToryNum];
                }
                if (Input.GetKeyUp(KeyCode.F))
                {
                    InvenToryNum++;
                    //총알 초기화
                    for (int i = 0; i < 30; i++)
                    {
                        SIngleTonData.instance.g_BulletData[i].GetComponent<GunBulletSCripts>().BulletStart = false;
                    }

                    if (InvenToryNum >= 4)
                    {
                        InvenToryNum = 0;
                    }

                }
                //shot 에 관한 것들
                if (SIngleTonData.instance.camNumber != 2)
                {
                    ShotBullet();
                }
                GunState();

                BasicPlayerMove();

                setEquirMent();

                SaveItem();

                CopyItemForUI();

                DropItem();

                // 에니메이션 전환용
            }
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
                localInvenTory[i].transform.rotation = Quaternion.Euler(90, 0,0);
                
            }
            else if (i == InvenToryNum && localInvenTory[InvenToryNum] != null)
            {
               
                localInvenTory[InvenToryNum].transform.SetParent(EqiirMentItemPosition.transform);
                //localInvenTory[InvenToryNum].transform.parent = EqiirMentItemPosition.transform;
                localInvenTory[InvenToryNum].transform.rotation = Player.transform.rotation;
                localInvenTory[InvenToryNum].transform.localPosition = Vector3.zero;
            }
        }     
    }

    void BasicPlayerMove()
    {
        if (Input.GetKey(KeyCode.W) && RunState == 0)
        {
            Player.transform.position += Player.transform.forward * 3.5f * DeltaTImeData.instance.DeltaTime;
            StartCoroutine("StartRun");
        }
        if (Input.GetKey(KeyCode.W) && RunState == 1)
        {
            Player.transform.position += Player.transform.forward * 3.5f * DeltaTImeData.instance.DeltaTime;
        }
        if (Input.GetKeyUp(KeyCode.W) && RunState == 1)
        {
            rigid.AddForce(Player.transform.forward * 3.55f, ForceMode.Impulse);
            StartCoroutine("EndRun");
        }
        ///////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.S) && BackState == 0)
        {
            Player.transform.position += Player.transform.forward * -2.0f * DeltaTImeData.instance.DeltaTime;
            StartCoroutine("StartBack");
        }
        if (Input.GetKey(KeyCode.S) && BackState == 1)
        {
            Player.transform.position += Player.transform.forward * -2.0f * DeltaTImeData.instance.DeltaTime;
            SIngleTonData.instance.CharacterBackMove = true;
        }
        if (Input.GetKeyUp(KeyCode.S) && BackState == 1)
        {
            rigid.AddForce(Player.transform.forward * -2.55f, ForceMode.Impulse);
            StartCoroutine("EndBack");
        }
        /////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.D) && RightState == 0)
        {
            Player.transform.position += Player.transform.right * 2.8f * DeltaTImeData.instance.DeltaTime;
            StartCoroutine("StartLeft");
        }
        if (Input.GetKey(KeyCode.D) && RightState == 1)
        {
            Player.transform.position += Player.transform.right * 2.8f * DeltaTImeData.instance.DeltaTime;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            rigid.AddForce(Player.transform.right * +2.5f, ForceMode.Impulse);
            StartCoroutine("EndLeft");
        }
        ////////////////////////////////////////////////////
        if (Input.GetKey(KeyCode.A) && LeftState == 0)
        {
            Player.transform.position += Player.transform.right * -2.8f * DeltaTImeData.instance.DeltaTime;
            StartCoroutine("StartRight");
        }
        if (Input.GetKey(KeyCode.A) && LeftState == 1)
        {
            Player.transform.position += Player.transform.right * -2.8f * DeltaTImeData.instance.DeltaTime;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            rigid.AddForce(Player.transform.right * -2.5f, ForceMode.Impulse);
            StartCoroutine("EndRight");
        }
        if (Input.GetKey(KeyCode.Space) && JumpState == 0)
        {
            animation.SetBool("Jump", true);
            rigid.velocity = Vector3.up * JumpPower;
            StartCoroutine("Jump");
        }

    }

    IEnumerator Jump()
    {
        JumpState = 1;
        yield return new WaitForSeconds(1.0f);
        JumpState = 0;
        animation.SetBool("Jump", false);
    }
    IEnumerator StartRun()
    {
        yield return new WaitForSeconds(0.0f);//0.7
        RunState = 1;
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("Run", true);
        }
        else
        {
            animation.SetBool("bGunRun", true);
        }

    }
    IEnumerator EndRun()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("RunEnd", true);
            animation.SetBool("Run", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("RunEnd", false);
        }
        else
        {
            animation.SetBool("bGunRunEnd", true);
            animation.SetBool("bGunRun", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("bGunRunEnd", false);
        }
        RunState = 0;
    }
    ////////////////////////////
    IEnumerator StartBack()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("Back", true);
        }
        else
        {
            animation.SetBool("bGunRun", true);
        }
        yield return new WaitForSeconds(0.0f);
        BackState = 1;

    }
    IEnumerator EndBack()
    {
        SIngleTonData.instance.CharacterBackMove = false;
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("BackEnd", true);
            animation.SetBool("Back", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("BackEnd", false);
        }
        else
        {
            animation.SetBool("bGunRunEnd", true);
            animation.SetBool("bGunRun", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("bGunRunEnd", false);
        }
        BackState = 0;
    }
    /////////////////////
    IEnumerator StartLeft()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("Left", true);
        }
        else
        {
            animation.SetBool("bGunRun", true);
        }
        yield return new WaitForSeconds(0.0f);
        LeftState = 1;

    }
    IEnumerator EndLeft()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("LeftEnd", true);
            animation.SetBool("Left", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("LeftEnd", false);
        }
        else
        {
            animation.SetBool("bGunRunEnd", true);
            animation.SetBool("bGunRun", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("bGunRunEnd", false);
        }
        LeftState = 0;
    }
    /////////////////////////////////
    IEnumerator StartRight()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("Right", true);
        }
        else
        {
            animation.SetBool("bGunRun", true);
        }
        yield return new WaitForSeconds(0.0f);
        RightState = 1;

    }
    IEnumerator EndRight()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            animation.SetBool("RightEnd", true);
            animation.SetBool("Right", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("RightEnd", false);
        }
        else
        {
            animation.SetBool("bGunRunEnd", true);
            animation.SetBool("bGunRun", false);
            yield return new WaitForSeconds(0.7f);
            animation.SetBool("bGunRunEnd", false);
        }
        RightState = 0;
    }

    //아이템 장착에 관한...
    void setEquirMent()
    {
        if (Input.GetMouseButtonUp(0) == true && ItemEatCheck == true)
        {
            if (EquirMentItem[1] != null)
            {
                //총알 초기화================================================================================
                for (int i = 0; i < 30; i++)
                {
                    SIngleTonData.instance.g_BulletData[i].GetComponent<GunBulletSCripts>().BulletStart = false;
                }
                //
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
                //총알 초기화================================================================================
                for (int i = 0; i < 30; i++)
                {
                    SIngleTonData.instance.g_BulletData[i].GetComponent<GunBulletSCripts>().BulletStart = false;
                }
                //
                localInvenTory[InvenToryNum].transform.parent = ItemFolder.transform;                              
                localInvenTory[InvenToryNum].transform.GetComponent<BoxCollider>().enabled = true;
                localInvenTory[InvenToryNum] = null;
                EquirMentItem[InvenToryNum] = null;
                SIngleTonData.instance.Inventory[InvenToryNum] = localInvenTory[InvenToryNum];
                //StartCoroutine(DropReturnfalse());
            }                     
        }
    }

    void GunState()
    {
        if (localInvenTory[InvenToryNum] == null)
        {
            GunStatePoint = 0;
            animation.SetBool("bGunIdle", false);
        }
        else if(localInvenTory[InvenToryNum] != null)
        {
            GunStatePoint = 1;
            animation.SetBool("bGunIdle", true);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
     
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            if (EquirMentItem[0] == null)
            {
                EquirMentItem[0] = col.gameObject;
            }
            else if (EquirMentItem[0] != null && EquirMentItem[0] != EquirMentItem[1])
            {
                EquirMentItem[1] = col.gameObject;
            }
            ItemEatCheck = true;
        }
        if (col.CompareTag("Heal"))
        {
            SIngleTonData.instance.HPBar += 2.0f;
            Destroy(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Item")
        {
            EquirMentItem[0] = null;
            ItemEatCheck = false;
        }
    }

    void OnCollisionExit(Collision col)
    {
       
    }

}
