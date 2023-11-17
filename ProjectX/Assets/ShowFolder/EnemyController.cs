using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    enum EnemyAtk
    {
        Warp,
        Homing,
        Dash,
        Down,
        Die,
        Num
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void FixedUpdate()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        Vector2 targetPos = player.transform.position;
        float x = targetPos.x;
        float y = 0f;
        Vector2 direction = new Vector2(x - transform.position.x, y).normalized;
        rb.velocity = direction * 5;
    }
}
