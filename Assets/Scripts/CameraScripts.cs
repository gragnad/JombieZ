using UnityEngine;
using System.Collections;

public class CameraScripts : MonoBehaviour {

    public Camera MainCAm = null;
    public Camera UIcamera = null;


    public GameObject Player;

    public GameObject PlayerHead;

    Vector3 PlayerPos;
    Transform PlayerTranform;

    Vector3 caculationPos;


    float PreHorizontal;
    float PreVertical;

    float Horizontal;
    float Vertical;

    Quaternion m_CharicterTarget;
    Quaternion m_CamraTarget;

    bool m_cursorIsLocked;

    int LongShotCheck = 0;
    int longEimCheck = 0;
    struct CamPosition
    {
        public Vector3 MaincamPos;
        public Vector3 FPScamPos;
        public Vector3 UiCamPos;
    };

    CamPosition camPos = new CamPosition();

	// Use this for initialization
	void Start () {

        //PlayerHead = Player.transform.FindChild("EthanHead").GetComponent<GameObject>();

    }
	void PlayerPosUpdate()
    {
        PlayerPos = Player.transform.position;
        PlayerTranform = Player.transform;

        //z 값이 앞뒤 이므로
        if(SIngleTonData.instance.camNumber == 0)
        {   
           
            Vector3 FollowMainCAm = PlayerTranform.forward * -2.0f;    
            caculationPos = new Vector3(PlayerPos.x + FollowMainCAm.x, PlayerPos.y+FollowMainCAm.y + 1.5f, PlayerPos.z + FollowMainCAm.z);
            MainCAm.fieldOfView = 60.0f;
            camPos.MaincamPos = caculationPos;
        }

        else if(SIngleTonData.instance.camNumber == 1)
        {   
           
            Vector3 FollowFpsCam = PlayerTranform.forward * +0.5f;
            caculationPos = new Vector3(PlayerPos.x + FollowFpsCam.x, PlayerPos.y+FollowFpsCam.y + 1.5f, PlayerPos.z + FollowFpsCam.z);
            MainCAm.fieldOfView = 40.0f;
            camPos.FPScamPos = caculationPos;          
        }
       
    }

	// Update is called once per frame
	void Update () {

        //위치
        PlayerPosUpdate();
        //로테이션
        FPSCAmraMoving();

        CamearaPosSetting();

    }

    void CamearaPosSetting()
    {
        if (SIngleTonData.instance.camNumber == 0)
        {
           MainCAm.transform.position = camPos.MaincamPos;
            UIcamera.transform.position = camPos.MaincamPos;
        }
        else if (SIngleTonData.instance.camNumber == 1)
        {
            MainCAm.transform.position = camPos.FPScamPos;
            UIcamera.transform.position = camPos.FPScamPos;
        }
    }

    //통일
    void FPSCAmraMoving()
    {
        if(SIngleTonData.instance.camNumber != 2)
        {
            PreHorizontal += Horizontal;
            PreVertical += Vertical;
            Horizontal = (float)Input.GetAxis("Mouse X") * 1.5f;
            Vertical = (float)Input.GetAxis("Mouse Y") * 1.5f;

            m_CamraTarget = Quaternion.Euler(-PreVertical, PreHorizontal, 0);
            m_CharicterTarget = Quaternion.Euler(0, PreHorizontal, 0);

            int x = Random.Range(-LongShotCheck, LongShotCheck);
            int y = Random.Range(-LongShotCheck, LongShotCheck);

            //무빙시 모션 블러 같은거 써야...끈기네...
            if (SIngleTonData.instance.ShotCheck == false)
            {
                LongShotCheck = 5;
                longEimCheck = 0;
            }
            else if(SIngleTonData.instance.ShotCheck == true)
            {
                if(SIngleTonData.instance.SniperCheck == false)
                {
                    m_CamraTarget = Quaternion.Euler(-PreVertical + (x * Time.deltaTime),
                    PreHorizontal + (y * Time.deltaTime), 0);
                    
                    longEimCheck++;
                    if (longEimCheck > 5)
                    {
                        longEimCheck = 0;
                        LongShotCheck += 5;
                        if (LongShotCheck > 50)
                        {
                            LongShotCheck = 50;
                        }
                    }
                }
                else if(SIngleTonData.instance.SniperCheck == true)
                {
                    m_CamraTarget = Quaternion.Euler(-PreVertical+(Time.deltaTime*-150.0f),PreHorizontal, 0);
                }
                Player.transform.rotation = m_CharicterTarget;
                MainCAm.transform.rotation = m_CamraTarget;        
            }


            UIcamera.transform.rotation = m_CamraTarget;

            //

            if (SIngleTonData.instance.camNumber == 0 && SIngleTonData.instance.ShotCheck == false)
            {
                Player.transform.rotation = m_CharicterTarget;
               MainCAm.transform.localRotation = Quaternion.Euler(-PreVertical, 0,0);             
                //PlayerHead.transform.localRotation = Quaternion.Euler(0,0, -PreVertical);
                //PlayerHead.transform.localRotation = m_CamraTarget;

            }
            else if(SIngleTonData.instance.camNumber == 1)
            {
                Player.transform.rotation = m_CamraTarget;
            }
           

            InternalLockUpdate();
        }       
    }
    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
