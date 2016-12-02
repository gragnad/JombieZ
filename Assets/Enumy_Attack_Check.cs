using UnityEngine;
using System.Collections;

public class Enumy_Attack_Check : MonoBehaviour
{

    ZombieEnumy CheckAttack;
    // Use this for initialization
    void Start()
    {
        CheckAttack = transform.parent.GetComponent<ZombieEnumy>();
    }

    void OnTriggerEnter(Collider colPlayer)
    {
        if (colPlayer.CompareTag("Player"))
        {
            CheckAttack.AttackStart = true;
        }
    }
}
