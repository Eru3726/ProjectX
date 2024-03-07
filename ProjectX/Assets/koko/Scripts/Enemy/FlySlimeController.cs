using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySlimeController : MonoBehaviour
{
    MoveController mc;

    [SerializeField]
    GameObject player;

    [SerializeField, Header("移動")]
    bool isMove = false;

    [SerializeField, Header("上下移動")]
    bool isUD = false;

    [SerializeField]
    float udSpd = 1;

    [SerializeField, Header("最大")]
    float maxSpd = 1;

    float nowSpd = 0;

    float udSign = 1;

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
        Vector3 scale = transform.localScale;
        scale.x = -plDir;
        transform.localScale = scale;

        if (isUD)
        {
            mc.InputUD(1);
            mc.moveSpd.y = nowSpd;
            nowSpd += Time.deltaTime * udSpd * udSign;
            if (Mathf.Abs(nowSpd) > maxSpd)
            {
                udSign *= -1;
            }
        }

        // ジャンプ移動
        if (isMove)
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
