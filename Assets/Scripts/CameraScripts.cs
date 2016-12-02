using UnityEngine;
using System.Collections;

public class CameraScripts : MonoBehaviour {

    public Camera MainCAm = null;
    public Camera UIcamera = null;
    public GameObject Player;

    public Light light = null;


    public float MaxCameraDistance;
    public float MinCameraDistance;

    Vector3 PlayerPos;
    Transform PlayerTranform;
    //
    public float CheckDistace;

    float DisTanceSpeed;

    Vector3 caculationPos;


    float PreHorizontal;
    float PreVertical;

    float Horizontal;
    float Vertical;

   public  Quaternion m_CharicterTarget;
    Quaternion m_CamraTarget;

    public bool m_cursorIsLocked;

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

        

    }

    void DisTanceSpeedCaculation()
    {
        if(0.0f < CheckDistace && 0.5f > CheckDistace)
        {
            DisTanceSpeed = 1.0f;
        }
        if (0.5f < CheckDistace && 1.5f > CheckDistace)
        {
            DisTanceSpeed = 2.0f;
        }
        if (1.5f < CheckDistace && 3.0f > CheckDistace)
        {
            DisTanceSpeed = 3.5f;
        }

    }

	void PlayerPosUpdate()
    {
        PlayerPos = Player.transform.position;
        PlayerTranform = Player.transform;
        Vector3 checklight = PlayerPos;
        checklight.y += 1.6f;
        light.transform.position = checklight;
        camPos.MaincamPos = MainCAm.transform.position;

        CheckDistace = Vector3.Distance(PlayerPos,camPos.MaincamPos);
        //거리에 따른 카메라 속도값
        DisTanceSpeedCaculation();
        //z 값이 앞뒤 이므로
        if (SIngleTonData.instance.camNumber == 0)
        {             
            //if(SIngleTonData.instance.CharacterBackMove == false)
            {
                MainCAm.fieldOfView = 60.0f;
                Vector3 DirctionPos = PlayerPos - camPos.MaincamPos;
                Vector3 FollowMainCam = PlayerTranform.forward * -2.0f;
                DirctionPos.x += FollowMainCam.x +0.5f;
                DirctionPos.y += FollowMainCam.y+1.5f;
                DirctionPos.z += FollowMainCam.z;
                camPos.MaincamPos += Time.deltaTime * DirctionPos* DisTanceSpeed;
            }

            //기본 제일 작을시 원래로 돌아 오는거

           /*if(SIngleTonData.instance.CharacterBackMove == true)
            {
                // MainCAm.transform.SetParent(Player.transform);
                Vector3 DirctionPos = PlayerPos - camPos.MaincamPos;
                Vector3 FollowMainCam = PlayerTranform.forward * -2.0f;
                DirctionPos.x += FollowMainCam.x;
                DirctionPos.y += FollowMainCam.y + 1.5f;
                DirctionPos.z += FollowMainCam.z;
                camPos.MaincamPos += Time.deltaTime * DirctionPos * DisTanceSpeed;
            }*/
            
        }

        else if(SIngleTonData.instance.camNumber == 1)
        {   
           //fps 는 그대로 가고 메인 캠만 따라오는거 체크
            Vector3 FollowFpsCam = PlayerTranform.forward * +0.5f;
            caculationPos = new Vector3(PlayerPos.x + FollowFpsCam.x, PlayerPos.y+FollowFpsCam.y + 1.5f, PlayerPos.z + FollowFpsCam.z);
            MainCAm.fieldOfView = 20.0f;
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
            else if(SIngleTonData.instance.ShotCheck == true && SIngleTonData.instance.Reload == false)
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
                 light.transform.rotation = m_CamraTarget;
            }


            UIcamera.transform.rotation = m_CamraTarget;

            //

            if (SIngleTonData.instance.camNumber == 0 && SIngleTonData.instance.ShotCheck == false)
            {
                Player.transform.rotation = m_CharicterTarget;
                //MainCAm.transform.localRotation = Quaternion.Euler(-PreVertical, 0,0);          
                MainCAm.transform.localRotation = m_CamraTarget;
                light.transform.rotation = m_CamraTarget;
            }
            else if(SIngleTonData.instance.camNumber == 1)
            {
                Player.transform.rotation = m_CamraTarget;
                MainCAm.transform.localRotation = m_CamraTarget;
                light.transform.rotation = m_CamraTarget;
            }
           

            InternalLockUpdate();
        }       
    }
    private void InternalLockUpdate()
    {
        
        if (Input.GetMouseButtonUp(0))
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
