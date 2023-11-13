using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk : MonoBehaviour
{
    public float AtkSpd = 5f;
    public float AtkRange = 5f;
    public float AtkCool = 2f;

    Transform player;
    bool canAtk = true;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {
        
    }

    public void EnemyAttack() //突進処理
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * AtkSpd * Time.deltaTime);
    }
}
