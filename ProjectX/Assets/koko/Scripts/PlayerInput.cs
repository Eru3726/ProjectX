using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [SerializeField, Header("NomalMeleeプレハブつけてね")]
    GameObject NMPre;

    [SerializeField, Header("LoveBeamプレハブつけてね")]
    GameObject LBPre;

    [SerializeField, Header("LoveMiaaileプレハブつけてね")]
    GameObject LMPre;

    [SerializeField, Header("AngerFireプレハブつけてね")]
    GameObject AFPre;

    [SerializeField, Header("アタッチしろ")]
    MoveController mc;

    [SerializeField, Header("アタッチ")]
    HitCollider hc;

    [SerializeField]
    List<bool> actSkill = new List<bool>();
    [SerializeField]
    List<float> skillTime = new List<float>();
    [SerializeField]
    List<float> coolTime = new List<float>();

    private void Start()
    {
        //mc = GetComponent<MoveController>();
        //hc = GetComponent<HitCollider>();

        for (int i = 0; i < (int)StageData.ACT_DATA.Num; i++)
        {
            actSkill.Add(false);
            skillTime.Add(0);
            coolTime.Add(0);
        }
    }

    private void Update()
    {
        // 移動処理（横移動、ジャンプ）
        MoveInput();

        // 向いてる方向
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        // NomalMelee : P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (skillTime[(int)StageData.ACT_DATA.NM3] > 0)
            {

            }
            else if (skillTime[(int)StageData.ACT_DATA.NM2] > 0)
            {
                if (actSkill[(int)StageData.ACT_DATA.NM3] == false)
                {
                    ActNM(3);
                    actSkill[(int)StageData.ACT_DATA.NM3] = true;
                    skillTime[(int)StageData.ACT_DATA.NM3] = 0.5f;
                    coolTime[(int)StageData.ACT_DATA.NM3] = 0.5f;
                }
            }
            else if (skillTime[(int)StageData.ACT_DATA.NM1] > 0)
            {
                if (actSkill[(int)StageData.ACT_DATA.NM2] == false)
                {
                    ActNM(2);
                    actSkill[(int)StageData.ACT_DATA.NM2] = true;
                    skillTime[(int)StageData.ACT_DATA.NM2] = 0.5f;
                    coolTime[(int)StageData.ACT_DATA.NM2] = 0.5f;
                }
            }
            else if (skillTime[(int)StageData.ACT_DATA.NM1] <= 0)
            {
                if (actSkill[(int)StageData.ACT_DATA.NM1] == false && !CheckActSkill())
                {
                    ActNM(1);
                    actSkill[(int)StageData.ACT_DATA.NM1] = true;
                    skillTime[(int)StageData.ACT_DATA.NM1] = 0.5f;
                    coolTime[(int)StageData.ACT_DATA.NM1] = 0.5f;
                }
            }
        }

        // NomalDodge : S or Shift
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (actSkill[(int)StageData.ACT_DATA.ND1] == false && !CheckActSkill())
            {
                ActND();
                actSkill[(int)StageData.ACT_DATA.ND1] = true;
                skillTime[(int)StageData.ACT_DATA.ND1] = 0.3f;
                coolTime[(int)StageData.ACT_DATA.ND1] = 1;
            }
        }

        // LoveBeam : B
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (actSkill[(int)StageData.ACT_DATA.LB1] == false && !CheckActSkill())
            {
                ActLB();
                actSkill[(int)StageData.ACT_DATA.LB1] = true;
                skillTime[(int)StageData.ACT_DATA.LB1] = 1;
                coolTime[(int)StageData.ACT_DATA.LB1] = 2;
            }
        }

        if (skillTime[(int)StageData.ACT_DATA.LB1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);
        }

        // LoveMissile : M
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (actSkill[(int)StageData.ACT_DATA.LM1] == false && !CheckActSkill())
            {
                ActLM();
                actSkill[(int)StageData.ACT_DATA.LM1] = true;
                skillTime[(int)StageData.ACT_DATA.LB1] = 0.5f;
                coolTime[(int)StageData.ACT_DATA.LB1] = 2;
            }
        }

        if (skillTime[(int)StageData.ACT_DATA.LM1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);
        }

        // AngerFire : F
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (actSkill[(int)StageData.ACT_DATA.AF1] == false && !CheckActSkill())
            {
                ActAF(0);
                actSkill[(int)StageData.ACT_DATA.AF1] = true;
                skillTime[(int)StageData.ACT_DATA.AF1] = 1;
                coolTime[(int)StageData.ACT_DATA.AF1] = 3;
            }
        }

        if (skillTime[(int)StageData.ACT_DATA.AF1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);

            if (skillTime[(int)StageData.ACT_DATA.AF1] <= 0.5f)
            {
                if (actSkill[(int)StageData.ACT_DATA.AF2] == false)
                {
                    ActAF(1);
                    actSkill[(int)StageData.ACT_DATA.AF2] = true;
                    skillTime[(int)StageData.ACT_DATA.AF2] = 0;
                    coolTime[(int)StageData.ACT_DATA.AF2] = coolTime[(int)StageData.ACT_DATA.AF1];
                }
            }
        }

        // SorrowBarrier : B
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (actSkill[(int)StageData.ACT_DATA.SB1] == false && !CheckActSkill())
            {
                ActSB();
                actSkill[(int)StageData.ACT_DATA.SB1] = true;
                skillTime[(int)StageData.ACT_DATA.SB1] = 0.5f;
                coolTime[(int)StageData.ACT_DATA.SB1] = 10;
            }
        }

        if (skillTime[(int)StageData.ACT_DATA.AF1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);
        }
    }

    private void FixedUpdate()
    {
        // スキル時間処理
        UpdateTime();
    }

    // スキルが起動中かどうか確認
    bool CheckActSkill()
    {
        float totalTime = 0;

        for (int i = 0; i < (int)StageData.ACT_DATA.Num; i++)
        {
            totalTime += skillTime[i];
        }

        if(totalTime > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void MoveInput()
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
    }

    void UpdateTime()
    {
        for (int i = 0; i < (int)StageData.ACT_DATA.Num; i++)
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
                actSkill[i] = false;
                coolTime[i] = 0;
            }
        }
    }

    void ActNM(int rush)
    {
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        GameObject obj = Instantiate(NMPre, plDir, Quaternion.identity);

        obj.transform.parent = this.transform;

        Vector3 scale = obj.transform.localScale;
        scale.y = (rush + 1) * 0.5f;
        obj.transform.localScale = scale;

        obj.GetComponent<AttackCollider>().atkType = StageData.ATK_DATA.NM1 + rush - 1;

        mc.InputFlick(plDir, (rush + 1) * 5, 0.2f, true);
    }

    void ActND()
    {
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        mc.InputFlick(plDir, 20, 0.2f, true);
        hc.SetInvTime(0.3f);
    }

    void ActLB()
    {
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x * 7.5f;

        GameObject obj = Instantiate(LBPre, plDir, Quaternion.identity);

        //mc.InputFlick(new Vector3(-plDir.x, plDir.y, plDir.z), 10, 0.3f, false);
    }

    void ActLM()
    {
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        GameObject obj = Instantiate(LMPre, plDir, Quaternion.identity);
        if (transform.localScale.x <= 0)
        {
            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = 180;
            obj.transform.localEulerAngles = lea;
        }
    }

    void ActAF(int num)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = Instantiate(AFPre, transform.position, Quaternion.identity);

            obj.GetComponent<AttackCollider>().atkType = StageData.ATK_DATA.AF1 + i + (num * 6);

            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = i * 60 + (num * 30);
            obj.transform.localEulerAngles = lea;
        }
    }

    void ActSB()
    {
        hc.SetBarrier(10);
    }
}
