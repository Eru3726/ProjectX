using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField, Header("パンチの当たり判定プレハブつけてね")]
    GameObject PunchPrefab;

    [SerializeField, Header("ファイヤーの当たり判定プレハブつけてね")]
    GameObject FirePrefab;

    [SerializeField, Header("アタッチしろ")]
    MoveController mc;

    [SerializeField, Header("アタッチ")]
    HitCollider hc;

    List<bool> inputSkill = new List<bool>();
    List<float> skillTime = new List<float>();

    // 0 攻撃1
    // 1 攻撃2
    // 2 攻撃3
    // 3
    // 4
    // 5 恋
    // 6 愛
    // 7
    // 8
    // 9
    // 10 怒攻撃1
    // 11 怒攻撃2
    // 12 怒攻撃3
    // 13 炎
    // 14 突進
    // 15 範囲
    // 16
    // 17
    // 18
    // 19
    // 20 回避
    // 21 バリア
    // 22 ブリンク
    // 23
    // 24

    private void Start()
    {
        //mc = GetComponent<MoveController>();
        //hc = GetComponent<HitCollider>();

        for (int i = 0; i < 25; i++)
        {
            inputSkill.Add(false);
            skillTime.Add(0);
        }
    }

    private void Update()
    {
        // 左右入力：A and D
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

        // 上入力：W or space
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (mc.IsGround())
            {
                mc.InputJump();
            }
        }

        // 向いてる方向
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        // 攻撃入力：P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (skillTime[2] > 0)
            {

            }
            else if (skillTime[1] > 0)
            {
                inputSkill[2] = true;
                skillTime[2] = 1;
            }
            else if (skillTime[0] > 0)
            {
                inputSkill[1] = true;
                skillTime[1] = 1;
            }
            else
            {
                inputSkill[0] = true;
                skillTime[0] = 1;
            }
        }

        // 攻撃処理
        if (inputSkill[0])
        {
            GameObject temp = Instantiate(PunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;

            mc.InputFlick(plDir, 10, 0.2f, true);

            inputSkill[0] = false;
        }
        else if (inputSkill[1])
        {
            GameObject temp = Instantiate(PunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;

            Vector3 scale = temp.transform.localScale;
            scale.y = 1.5f;
            temp.transform.localScale = scale;

            mc.InputFlick(plDir, 15, 0.2f, true);

            inputSkill[1] = false;
        }
        else if (inputSkill[2])
        {
            GameObject temp = Instantiate(PunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;

            Vector3 scale = temp.transform.localScale;
            scale.y = 2f;
            temp.transform.localScale = scale;

            mc.InputFlick(plDir, 20, 0.2f, true);

            inputSkill[2] = false;
        }

        // 回避：S or Shift
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            inputSkill[20] = true;
            skillTime[20] = 0.1f;

            mc.InputFlick(plDir, 20, 0.1f, true);
            hc.SetInvTime(0.3f);

            inputSkill[20] = false;
        }

        // スキル１火炎：F
        if (Input.GetKeyDown(KeyCode.F))
        {

        }

        // スキル３バリア：B
        if (Input.GetKeyDown(KeyCode.B))
        {

        }
    }

    private void FixedUpdate()
    {
        // スキル時間処理
        for (int i = 0; i < 25; i++)
        {
            if (skillTime[i] > 0)
            {
                skillTime[i] -= Time.deltaTime;
            }
        }
    }
}
