using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtk : MonoBehaviour
{
    Transform player;
    EnemyController EnmCon;

    MoveController mc;

    public float ChargeForce = 10f;
    public int count = 0;
    Rigidbody2D rb;

    void Start()
    {
        EnmCon = gameObject.GetComponent<EnemyController>();
        player = GameObject.Find("Player").transform;
        rb = gameObject.GetComponent<Rigidbody2D>();

        mc = GetComponent<MoveController>();
    }

    
    void Update()
    {
        
    }

    public void EnemyAttack() //突進処理
    {
        //Vector3 chargeDir = Vector3.left;
        //rb.AddForce(chargeDir * ChargeForce);
        //count++;
        //mc.InputJump();
    }
}
