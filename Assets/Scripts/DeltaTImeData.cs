using UnityEngine;
using System.Collections;

public class DeltaTImeData
{
    private static DeltaTImeData InsTance = null;
    public static DeltaTImeData instance
    {
        get
        {
            if(InsTance == null)
            {
            InsTance = new DeltaTImeData();
            }

            return InsTance;
        }

    }
    private DeltaTImeData()
    {

    }

    public bool DelataTimeCheck = true;
    //싱글턴으로 따로 인수로 등록하여 일시정지를 조절한다!!!
    public float DeltaTime;
    public float DeltaTimeScale;
   
    public void TimeClassDeltaTime()
    {
        if(DelataTimeCheck == true)
        {
            DeltaTime = Time.deltaTime; 
            DeltaTimeScale = Time.timeScale;
        }
        else if(DelataTimeCheck == false)
        {
            DeltaTime = 0.0f;
            DeltaTimeScale = 0.0f;
        }
    }

  
	// Use this for initialization
	
}
