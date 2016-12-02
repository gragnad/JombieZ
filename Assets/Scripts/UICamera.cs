using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UICamera : MonoBehaviour {

    //버튼 클릭시 넘겨주는 걸로///

    public Camera MainCam = null;
    public Camera UICamrach = null;

    public Button Checkbtn1;
    public Button Checkbtn2;
    public Button Checkbtn3;
    public Button Checkbtn4;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;


   public  Image changemat;
    //
    //
    /*struct UIPos
    {
        public Vector3 up;
        public Vector3 down;
        public Vector3 left;
        public Vector3 right;

    }*/

    struct UIIven
    {
        public GameObject itemprefeb;
        public int itemNumber;
        public int PosNumber;
        public int Bullet;
    }
    
    GameObject[] ItemPrefebs = new GameObject[3];

     Material[] UIMAT = new Material[4];

   // Material MAT = new Material();

    UIIven[] uinven = new UIIven[4];
    
	// Use this for initialization
	void Start () {
        UICamrach.enabled = false;
        MainCam.enabled = true;

        UIMAT[0] = Resources.Load("M_Gun", typeof(Material)) as Material;
        UIMAT[1] = Resources.Load("M_Bonb", typeof(Material)) as Material;
        UIMAT[2] = Resources.Load("M_Sna", typeof(Material)) as Material;
        UIMAT[3] = Resources.Load("M_Non", typeof(Material)) as Material;

    }
	/*void UIPOSUpdate()
    {
        Vector3 UIPos = UICamrach.transform.position;

        uipos.up = new Vector3(UIPos.x, UIPos.y + 0.8f, UIPos.z +2f);
        uipos.down = new Vector3(UIPos.x, UIPos.y + -0.8f, UIPos.z +2f);
        uipos.left = new Vector3(UIPos.x + -1.0f, UIPos.y, UIPos.z +2f);
        uipos.right = new Vector3(UIPos.x + 1.0f, UIPos.y, UIPos.z +2f);

    }*/

    // Update is called once per frame
    void Update () {
        ChangeCamera();

       SigleToeDataCopy();

        CheckImage();
        //UIPOSUpdate();

        Checkbtn1.onClick.AddListener(btn1Click);
        Checkbtn2.onClick.AddListener(btn2Click);
        Checkbtn3.onClick.AddListener(btn3Click);
        Checkbtn4.onClick.AddListener(btn4Click);

       

    }

    //좀더 생각해보자...
    private void ChangeImgeSetPos()
    {
        Vector3 CheckPos = transform.position;
        CheckPos.y -= -6;
        CheckPos.z += 3;
        changemat.transform.position = CheckPos;
    }

    private void CheckImage()
    {
       for(int i =0;i<4;i++)
        {
            if(uinven[i].itemprefeb != null)
            {
                //위치
                //아이템 넘버에 따른 이미지
                Pos(uinven[i].PosNumber, uinven[i].itemNumber);

                SettextBullet(uinven[i].PosNumber,uinven[i].Bullet);


            }
            else if(uinven[i].itemprefeb == null)
            {
                nullPos(uinven[i].PosNumber);
            }
        }
    }

    private void SettextBullet(int IvenPos, int bulletCount)
    {
        if(bulletCount <= 0)
        {
            bulletCount = 0;
        }
        switch (IvenPos)
        {
            case 0:
                text1.text = string.Format("{0}",bulletCount);
                break;
            case 1:
                text2.text = string.Format("{0}", bulletCount);
                break;
            case 2:
                text3.text = string.Format("{0}", bulletCount);
                break;
            case 3:
                text4.text = string.Format("{0}", bulletCount);
                break;
        }
    }


    private void Pos(int IvenPos,int ItemNumber)
    {
        switch(IvenPos)
        {
            case 0:
                ItemImage(Checkbtn1, ItemNumber);             
              
                break;
            case 1:
                ItemImage(Checkbtn2, ItemNumber);
               
                break;
            case 2:
                ItemImage(Checkbtn3, ItemNumber);
              
                break;
            case 3:
                ItemImage(Checkbtn4, ItemNumber);
                
                break;
        }
    }
    private void ItemImage(Button btn,int ItemNUmber)
    {
        
        switch (ItemNUmber)
        {
            case 0:
                changemat = btn.GetComponent<Image>();
                
                changemat.material = UIMAT[0];
                break;
            case 1:
                changemat = btn.GetComponent<Image>();
               
                changemat.material = UIMAT[1];
                break;
            case 2:
                changemat = btn.GetComponent<Image>();
                
                changemat.material = UIMAT[2];
                break;
        }
    }

    private void nullPos(int IvenPos)
    {

        switch (IvenPos)
        {
            case 0:
                changemat = Checkbtn1.GetComponent<Image>();
                changemat.material = UIMAT[3];
                break;
            case 1:
                changemat = Checkbtn2.GetComponent<Image>();
                changemat.material = UIMAT[3];
                break;
            case 2:
                changemat = Checkbtn3.GetComponent<Image>();
                changemat.material = UIMAT[3];
                break;
            case 3:
                changemat = Checkbtn4.GetComponent<Image>();
                changemat.material = UIMAT[3];
                break;
        }
    }

    private void btn1Click()
    {
        SIngleTonData.instance.InvenToryNumber = 0;
        SIngleTonData.instance.UiChangeItem = true;
    }
    private void btn2Click()
    {
        SIngleTonData.instance.InvenToryNumber = 1;
        SIngleTonData.instance.UiChangeItem = true;
    }
    private void btn3Click()
    {
        SIngleTonData.instance.InvenToryNumber = 2;
        SIngleTonData.instance.UiChangeItem = true;
    }
    private void btn4Click()
    {
        SIngleTonData.instance.InvenToryNumber = 3;
        SIngleTonData.instance.UiChangeItem = true;
    }

    void ChangeCamera()
    {
        if(SIngleTonData.instance.camNumber == 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            DeltaTImeData.instance.DelataTimeCheck = false;
            DeltaTImeData.instance.TimeClassDeltaTime();

            UICamrach.enabled = true;
            MainCam.enabled = false;
        }
        if (SIngleTonData.instance.camNumber != 2)
        {
            DeltaTImeData.instance.DelataTimeCheck = true;
            DeltaTImeData.instance.TimeClassDeltaTime();

            UICamrach.enabled = false;
            MainCam.enabled = true;
                            
        }
    }

    void SigleToeDataCopy()
    {
        int num = SIngleTonData.instance.InvenToryNumber;

        if (SIngleTonData.instance.Inventory[num] != null)
        {
            uinven[num].itemprefeb = SIngleTonData.instance.Inventory[num];
            uinven[num].itemNumber = SIngleTonData.instance.Inventory[num].GetComponent<ItemScripts>().ItemNumber;
            uinven[num].Bullet = SIngleTonData.instance.Inventory[num].GetComponent<ItemScripts>().BulletCount;
            uinven[num].PosNumber = num;
        }
        else if(SIngleTonData.instance.Inventory[num] == null)
        {
            uinven[num].itemprefeb = null;
            uinven[num].PosNumber = num;
        }
        
    }

    /*void UISeeCamera()
    {
        for (int i = 0; i < 4; i++)
        {
            if (uinven[i].itemprefeb != null)
            {
                int check = uinven[i].PosNumber;
                switch (check)
                {
                    //포지션값들을 받고
                    case 0:
                        Position(uinven[i].itemNumber, check);
                        break;
                    case 1:
                        Position(uinven[i].itemNumber, check);
                        break;
                    case 2:
                        Position(uinven[i].itemNumber, check);
                        break;
                    case 3:
                        Position(uinven[i].itemNumber, check);
                        break;
                }
            }
        }     
    }

    void Position(int itemNUmber,int CheckPos)
    {            
        if(itemNUmber == 0)
        {
            copy1[CheckPos] = Instantiate(gun);     
        }
        else if (itemNUmber == 1)
        {
            copy1[CheckPos] = Instantiate(bomb);
        }
        else if (itemNUmber == 2)
        {
            copy1[CheckPos] = Instantiate(sniffer);
        }
        copy1[CheckPos].transform.Rotate(0, 90, 0);
       
        SetPosition(CheckPos);
    }

    void SetPosition(int CheckPos)
    {
        switch (CheckPos)
        {
            case 0:
                copy1[CheckPos].transform.position = uipos.up;
                break;
            case 1:
                copy1[CheckPos].transform.position = uipos.right;
                break;
            case 2:
                copy1[CheckPos].transform.position = uipos.down;
                break;
            case 3:
                copy1[CheckPos].transform.position = uipos.left;
                break;
        }
    }*/

}
