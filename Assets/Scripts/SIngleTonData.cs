using UnityEngine;
using System.Collections;

public class SIngleTonData
{
    private static SIngleTonData Instance = null;
    public static SIngleTonData instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = new SIngleTonData();
             }
            return Instance;
        }
    } 
    private SIngleTonData()
    {

    }

    //타임 받아오기
    public int Min;
    public float SeCond;

    //카메라 넘버 넘겨만 주면 카메라 변하게 플레이어 에서
    //0 번 TPS 1번 저격 2번 uicamera
    public int camNumber = 0;
    //

    //
    public bool UiChangeItem = false;
    //
    public GameObject[] Inventory = new GameObject[4];
    public int InvenToryNumber = 0;

}
