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

    [SerializeField]
    List<bool> inputSkill = new List<bool>();
    [SerializeField]
    List<float> skillTime = new List<float>();
    [SerializeField]
    List<float> coolTime = new List<float>();

    private void Start()
    {
        //mc = GetComponent<MoveController>();
        //hc = GetComponent<HitCollider>();

        for (int i = 0; i < (int)StageData.SKILL_DATA.Num; i++)
        {
            inputSkill.Add(false);
            skillTime.Add(0);
            coolTime.Add(0);
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
            if (skillTime[(int)StageData.SKILL_DATA.NM3] > 0)
            {

            }
            else if (skillTime[(int)StageData.SKILL_DATA.NM2] > 0)
            {
                inputSkill[(int)StageData.SKILL_DATA.NM3] = true;
                skillTime[(int)StageData.SKILL_DATA.NM3] = 1;
            }
            else if (skillTime[(int)StageData.SKILL_DATA.NM1] > 0)
            {
                inputSkill[(int)StageData.SKILL_DATA.NM2] = true;
                skillTime[(int)StageData.SKILL_DATA.NM2] = 1;
            }
            else
            {
                inputSkill[(int)StageData.SKILL_DATA.NM1] = true;
                skillTime[(int)StageData.SKILL_DATA.NM1] = 1;
            }
        }

        // 攻撃処理
        if (inputSkill[(int)StageData.SKILL_DATA.NM1])
        {
            GameObject temp = Instantiate(PunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;

            temp.GetComponent<AttackCollider>().atkType = StageData.ATK_DATA.NM1;

            mc.InputFlick(plDir, 10, 0.2f, true);

            inputSkill[(int)StageData.SKILL_DATA.NM1] = false;
        }
        else if (inputSkill[(int)StageData.SKILL_DATA.NM2])
        {
            GameObject temp = Instantiate(PunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;

            Vector3 scale = temp.transform.localScale;
            scale.y = 1.5f;
            temp.transform.localScale = scale;

            temp.GetComponent<AttackCollider>().atkType = StageData.ATK_DATA.NM2;

            mc.InputFlick(plDir, 15, 0.2f, true);

            inputSkill[(int)StageData.SKILL_DATA.NM2] = false;
        }
        else if (inputSkill[(int)StageData.SKILL_DATA.NM3])
        {
            GameObject temp = Instantiate(PunchPrefab, plDir, Quaternion.identity);
            temp.transform.parent = this.transform;

            temp.GetComponent<AttackCollider>().atkType = StageData.ATK_DATA.NM3;

            Vector3 scale = temp.transform.localScale;
            scale.y = 2f;
            temp.transform.localScale = scale;

            mc.InputFlick(plDir, 20, 0.2f, true);

            inputSkill[(int)StageData.SKILL_DATA.NM3] = false;
        }



        // 回避：S or Shift
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (coolTime[(int)StageData.SKILL_DATA.ND1] <= 0)
            {
                inputSkill[(int)StageData.SKILL_DATA.ND1] = true;
                skillTime[(int)StageData.SKILL_DATA.ND1] = 0.1f;
                coolTime[(int)StageData.SKILL_DATA.ND1] = 1;
            }
        }

        if (inputSkill[(int)StageData.SKILL_DATA.ND1])
        {
            mc.InputFlick(plDir, 20, 0.1f, true);
            hc.SetInvTime(0.3f);

            inputSkill[(int)StageData.SKILL_DATA.ND1] = false;
        }



        // スキル１火炎：F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (coolTime[(int)StageData.SKILL_DATA.AF1] <= 0)
            {

                inputSkill[(int)StageData.SKILL_DATA.AF1] = true;
                skillTime[(int)StageData.SKILL_DATA.AF1] = 0.5f;
                coolTime[(int)StageData.SKILL_DATA.AF1] = 3;

                for (int i = 0; i < 6; i++)
                {
                    GameObject temp = Instantiate(FirePrefab, transform.position, Quaternion.identity);
                    Vector3 lea = temp.transform.localEulerAngles;
                    lea.z = i * 60;
                    temp.transform.localEulerAngles = lea;
                }

            }
        }

        if (inputSkill[(int)StageData.SKILL_DATA.AF1])
        {
            if (skillTime[(int)StageData.SKILL_DATA.AF1] <= 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject temp = Instantiate(FirePrefab, transform.position, Quaternion.identity);
                    Vector3 lea = temp.transform.localEulerAngles;
                    lea.z = i * 60 + 30;
                    temp.transform.localEulerAngles = lea;
                }
                inputSkill[(int)StageData.SKILL_DATA.AF1] = false;
            }
            else { mc.InputFlickStop(); }
        }
    }

    private void FixedUpdate()
    {
        // スキル時間処理
        for (int i = 0; i < (int)StageData.SKILL_DATA.Num; i++)
        {
            if (skillTime[i] > 0)
            {
                skillTime[i] -= Time.deltaTime;
            }
            else
            {
                skillTime[i] = 0;
            }

            if (coolTime[i] > 0)
            {
                coolTime[i] -= Time.deltaTime;
            }
            else
            {
                coolTime[i] = 0;
            }
        }
    }
}
