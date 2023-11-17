using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField, Header("パンチの当たり判定をつけてね")]
    GameObject PlayerPunchPrefab;

    [SerializeField]
    MoveController mc;
    [SerializeField]
    HitCollider hc;

    bool inputDodge = false;
    float dodgeTime = 0;

    private void Start()
    {
        //mc = GetComponent<MoveController>();
        //hc = GetComponent<HitCollider>();
    }

    private void Update()
    {
        // 左右入力
        if (Input.GetKey(KeyCode.D)) { mc.InputLR(1); }
        else if (Input.GetKey(KeyCode.A)) { mc.InputLR(-1); }
        else { mc.InputLR(0); }

        // キャラの向き変更
        if (mc.GetLR() != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * mc.GetLR();
            transform.localScale = scale;
        }

        // 上入力
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (mc.gc.IsGround())
            {
                mc.InputJump();
                return;
            }
        }

        // 向いてる方向
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        // 攻撃
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameObject temp = Instantiate(PlayerPunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;
        }

        // 回避
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            inputDodge = true;
            dodgeTime = 0.1f;

            mc.InputFlick(plDir, 20, 0.1f);
            hc.SetInvTime(0.3f);
        }

        // 空中停止
        if(Input.GetKeyDown(KeyCode.T))
        {
            mc.InputFlickStop();
        }

        // 回避後処理
        if (dodgeTime >= 0)
        {
            dodgeTime -= Time.deltaTime;
        }
        else
        {
            if(inputDodge)
            {
                mc.InputFlickStop();
                inputDodge = false;
            }
        }
    }
}
