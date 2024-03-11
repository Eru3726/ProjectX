using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    MoveController mc;

    [SerializeField]
    GameObject player;

    [SerializeField, Header("向き変更")]
    bool isTurn = true;

    [SerializeField, Header("飛んでる時移動")]
    bool isMove = false;

    [SerializeField, Header("ジャンプ")]
    bool isJump = false;

    [SerializeField, Header("ジャンプディレイ")]
    float jumpDelay = 0.5f;

    float jumpTimer = 0;


    private void Start()
    {
        mc = GetComponent<MoveController>();

        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        // プレイヤー方向確認
        int plDir = (player.transform.position.x > transform.position.x ? 1 : -1);

        // 自分の向き変更
        if (isTurn)
        {
            Vector3 scale = transform.localScale;
            scale.x = -plDir;
            transform.localScale = scale;
        }

        // ジャンプ
        if (isJump)
        {
            if (mc.IsGround())
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer >= jumpDelay)
                {
                    mc.InputJump();
                    jumpTimer = 0;
                }
            }
        }

        // ジャンプ移動
        if (isMove)
        {
            if (mc.IsGround())
            {
                mc.InputLR(0);
            }
            else
            {
                if (player.transform.position.x > transform.position.x)
                {
                    mc.InputLR(plDir);
                }
                else
                {
                    mc.InputLR(plDir);
                }
            }
        }
    }
}
