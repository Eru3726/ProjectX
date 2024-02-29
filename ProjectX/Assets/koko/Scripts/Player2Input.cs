using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Input : MonoBehaviour
{

    [SerializeField, Header("NomalMeleeプレハブつけてね")]
    GameObject NMPre;

    [SerializeField, Header("LoveBeamプレハブつけてね")]
    GameObject LBPre;

    [SerializeField, Header("LoveMiaaileプレハブつけてね")]
    GameObject LMPre;

    [SerializeField, Header("AngerFireプレハブつけてね")]
    GameObject AFPre;

    [SerializeField, Header("AngerChargeプレハブつけてね")]
    GameObject ACPre;

    [SerializeField, Header("AngerAreaプレハブつけてね")]
    GameObject AAPre;

    [SerializeField, Header("MoveControllerアタッチ")]
    MoveController mc;

    [SerializeField, Header("HitColliderアタッチ")]
    OldHitCollider hc;

    public int piInputLR = 0;

    [SerializeField]
    public List<bool> actSkill = new List<bool>();
    [SerializeField]
    public List<float> skillTime = new List<float>();
    [SerializeField]
    public List<float> coolTime = new List<float>();

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

        // NomalMelee : P
        if (Input.GetKeyDown(KeyCode.Return))
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
                    coolTime[(int)StageData.ACT_DATA.NM1] = 0.75f;
                }
            }
            else if (skillTime[(int)StageData.ACT_DATA.NM1] > 0)
            {
                if (actSkill[(int)StageData.ACT_DATA.NM2] == false)
                {
                    ActNM(2);
                    actSkill[(int)StageData.ACT_DATA.NM2] = true;
                    skillTime[(int)StageData.ACT_DATA.NM2] = 0.5f;
                    coolTime[(int)StageData.ACT_DATA.NM1] = 0.75f;
                }
            }
            else if (skillTime[(int)StageData.ACT_DATA.NM1] <= 0)
            {
                if (actSkill[(int)StageData.ACT_DATA.NM1] == false && !CheckActSkill())
                {
                    ActNM(1);
                    actSkill[(int)StageData.ACT_DATA.NM1] = true;
                    skillTime[(int)StageData.ACT_DATA.NM1] = 0.5f;
                    coolTime[(int)StageData.ACT_DATA.NM1] = 0.75f;
                }
            }
        }

        // NomalDodge : S or Shift
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (actSkill[(int)StageData.ACT_DATA.ND1] == false && !CheckActSkill())
            {
                ActND();
                actSkill[(int)StageData.ACT_DATA.ND1] = true;
                skillTime[(int)StageData.ACT_DATA.ND1] = 0.3f;
                coolTime[(int)StageData.ACT_DATA.ND1] = 1;
            }
        }

        //// LoveBeam : L
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    if (actSkill[(int)StageData.ACT_DATA.LB1] == false && !CheckActSkill())
        //    {
        //        ActLB();
        //        actSkill[(int)StageData.ACT_DATA.LB1] = true;
        //        skillTime[(int)StageData.ACT_DATA.LB1] = 1;
        //        coolTime[(int)StageData.ACT_DATA.LB1] = 2;
        //    }
        //}

        //if (skillTime[(int)StageData.ACT_DATA.LB1] > 0)
        //{
        //    mc.InputFlickStop();
        //    mc.InputLR(0);
        //}

        //// LoveMissile : M
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    if (actSkill[(int)StageData.ACT_DATA.LM1] == false && !CheckActSkill())
        //    {
        //        ActLM(1);
        //        ActLM(2);
        //        ActLM(3);
        //        ActLM(4);
        //        ActLM(5);
        //        ActLM(6);
        //        actSkill[(int)StageData.ACT_DATA.LM1] = true;
        //        skillTime[(int)StageData.ACT_DATA.LB1] = 0.5f;
        //        coolTime[(int)StageData.ACT_DATA.LB1] = 2;
        //    }
        //}

        //if (skillTime[(int)StageData.ACT_DATA.LM1] > 0)
        //{
        //    mc.InputFlickStop();
        //    mc.InputLR(0);
        //}

        //// AngerFire : F
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (actSkill[(int)StageData.ACT_DATA.AF1] == false && !CheckActSkill())
        //    {
        //        ActAF(0);
        //        actSkill[(int)StageData.ACT_DATA.AF1] = true;
        //        skillTime[(int)StageData.ACT_DATA.AF1] = 1;
        //        coolTime[(int)StageData.ACT_DATA.AF1] = 3;
        //    }
        //}

        //if (skillTime[(int)StageData.ACT_DATA.AF1] > 0)
        //{
        //    mc.InputFlickStop();
        //    mc.InputLR(0);

        //    if (skillTime[(int)StageData.ACT_DATA.AF1] <= 0.5f)
        //    {
        //        if (actSkill[(int)StageData.ACT_DATA.AF2] == false)
        //        {
        //            ActAF(1);
        //            actSkill[(int)StageData.ACT_DATA.AF2] = true;
        //            skillTime[(int)StageData.ACT_DATA.AF2] = 0;
        //            coolTime[(int)StageData.ACT_DATA.AF2] = coolTime[(int)StageData.ACT_DATA.AF1];
        //        }
        //    }
        //}

        //// AngerCharge : C
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (actSkill[(int)StageData.ACT_DATA.AC1] == false && !CheckActSkill())
        //    {
        //        ActAC();
        //        actSkill[(int)StageData.ACT_DATA.AC1] = true;
        //        skillTime[(int)StageData.ACT_DATA.AC1] = 2;
        //        coolTime[(int)StageData.ACT_DATA.AC1] = 3;
        //    }
        //}

        //if (skillTime[(int)StageData.ACT_DATA.AC1] > 0)
        //{
        //    mc.InputFlick((int)transform.localScale.x, 10, 0, false);
        //}

        //// AngerArea : O
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    if (actSkill[(int)StageData.ACT_DATA.AA1] == false && !CheckActSkill())
        //    {
        //        ActAA();
        //        actSkill[(int)StageData.ACT_DATA.AA1] = true;
        //        skillTime[(int)StageData.ACT_DATA.AA1] = 2;
        //        coolTime[(int)StageData.ACT_DATA.AA1] = 10;
        //    }
        //}

        //if (skillTime[(int)StageData.ACT_DATA.AA1] > 0)
        //{
        //    mc.InputFlickStop();
        //    mc.InputLR(0);
        //}

        //// SorrowBarrier : B
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    if (actSkill[(int)StageData.ACT_DATA.SB1] == false && !CheckActSkill())
        //    {
        //        ActSB();
        //        actSkill[(int)StageData.ACT_DATA.SB1] = true;
        //        skillTime[(int)StageData.ACT_DATA.SB1] = 0.5f;
        //        coolTime[(int)StageData.ACT_DATA.SB1] = 10;
        //    }
        //}

        //if (skillTime[(int)StageData.ACT_DATA.SB1] > 0)
        //{
        //    mc.InputFlickStop();
        //    mc.InputLR(0);
        //}
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

        if (totalTime > 0)
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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            mc.InputLR(1);
            piInputLR = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            mc.InputLR(-1);
            piInputLR = -1;
        }
        else
        {
            mc.InputLR(0);
            piInputLR = 0;
        }

        // キャラの向き変更
        if (mc.GetLR() != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * mc.GetLR();
            transform.localScale = scale;
        }

        // 上入力：W or space
        if (Input.GetKey(KeyCode.UpArrow))
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

    void ActNM(int rushNo)
    {
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        GameObject obj = Instantiate(NMPre, plDir, Quaternion.identity);

        obj.transform.parent = this.transform;

        Vector3 scale = obj.transform.localScale;
        scale.y = (rushNo + 1) * 0.5f;
        obj.transform.localScale = scale;

        obj.GetComponent<OldAttackCollider>().atkType = StageData.ATK_DATA.NM1 + rushNo - 1;
        obj.GetComponent<OldAttackCollider>().atkLayer = StageData.LAYER_DATA.Enemy;

        plDir.y += 0.1f;
        if (rushNo == 3) { plDir.y += 0.4f; }

        mc.InputFlick(plDir, (rushNo + 1) * 3, 0.2f, true);
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

        obj.transform.parent = this.transform;

        //mc.InputFlick(new Vector3(-plDir.x, plDir.y, plDir.z), 10, 0.3f, false);
    }

    void ActLM(int num)
    {
        Vector3 plDir = this.transform.position;
        plDir.x -= transform.localScale.x;

        float dist = 1;
        Vector3 startPos = new Vector3(plDir.x, plDir.y + (dist * (3.5f - num)), plDir.z);
        GameObject obj = Instantiate(LMPre, startPos, Quaternion.identity);

        obj.GetComponent<OldAttackCollider>().atkType = StageData.ATK_DATA.LM1 + (num - 1);

        float delay;
        if (num == 1 || num == 6) { delay = 3; }
        else if (num == 2 || num == 5) { delay = 2; }
        else if (num == 3 || num == 4) { delay = 1; }
        else { delay = 0; }
        obj.GetComponent<MissileController>().delayCount = 0.1f * delay;

        if (transform.localScale.x <= 0)
        {
            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = 180 - (((float)num - 3.5f) * 5);
            obj.transform.localEulerAngles = lea;
        }
        else
        {
            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = (((float)num - 3.5f) * 5);
            obj.transform.localEulerAngles = lea;
        }
    }

    void ActAF(int num)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = Instantiate(AFPre, transform.position, Quaternion.identity);

            obj.GetComponent<OldAttackCollider>().atkType = StageData.ATK_DATA.AF1 + i + (num * 6);

            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = i * 60 + (num * 30);
            obj.transform.localEulerAngles = lea;
        }
    }

    void ActAC()
    {
        GameObject obj = Instantiate(ACPre, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
    }

    void ActAA()
    {
        GameObject obj = Instantiate(AAPre, transform.position, Quaternion.identity);
        obj.transform.parent = this.transform;
    }

    void ActSB()
    {
        hc.SetBarrier(10);
    }
}