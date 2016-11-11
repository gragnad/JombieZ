using UnityEngine;
using System.Collections;

public class CameraScripts : MonoBehaviour {

    public Camera MainCAm = null;
    public Camera UIcamera = null;


    public Transform Player;

    Vector3 PlayerPos;
    Transform PlayerTranform;


    float PreHorizontal;
    float PreVertical;

    float Horizontal;
    float Vertical;

    Quaternion m_CharicterTarget;
    Quaternion m_CamraTarget;

    bool m_cursorIsLocked;

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
	void PlayerPosUpdate()
    {
        PlayerPos = Player.transform.position;
        PlayerTranform = Player.transform;

        //z 값이 앞뒤 이므로
        if(SIngleTonData.instance.camNumber == 0)
        {   
            camPos.MaincamPos = PlayerPos;
            Vector3 FollowMainCAm = PlayerTranform.forward * -2.0f;          
            camPos.MaincamPos = new Vector3(PlayerPos.x + FollowMainCAm.x, FollowMainCAm.y+1.5f, PlayerPos.z+ FollowMainCAm.z);      
            MainCAm.transform.position = camPos.MaincamPos;
            UIcamera.transform.position = camPos.MaincamPos;
        }

        else if(SIngleTonData.instance.camNumber == 1)
        {   
           camPos.FPScamPos = PlayerPos;
            Vector3 FollowFpsCam = PlayerTranform.forward * +0.5f;
            camPos.FPScamPos = new Vector3(PlayerPos.x + FollowFpsCam.x, FollowFpsCam.y + 1.5f, PlayerPos.z + FollowFpsCam.z);
            MainCAm.transform.position = camPos.FPScamPos;
            UIcamera.transform.position = camPos.FPScamPos;
        }
       
    }

	// Update is called once per frame
	void Update () {

        PlayerPosUpdate();
        FPSCAmraMoving();

    }

    //통일
    void FPSCAmraMoving()
    {
        if(SIngleTonData.instance.camNumber != 2)
        {
            PreHorizontal += Horizontal;
            PreVertical += Vertical;
            Horizontal = (float)Input.GetAxis("Mouse X") * 2.0f;
            Vertical = (float)Input.GetAxis("Mouse Y") * 2.0f;

            m_CamraTarget = Quaternion.Euler(-PreVertical, PreHorizontal, 0);
            m_CharicterTarget = Quaternion.Euler(0, PreHorizontal, 0);

            //무빙시 모션 블러 같은거 써야...끈기네...
            MainCAm.transform.rotation = m_CamraTarget;
            UIcamera.transform.rotation = m_CamraTarget;
            Player.transform.rotation = m_CharicterTarget;

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
