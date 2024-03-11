using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    Mng_Game gameMng;

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

    [SerializeField, Header("BarrierEffectプレハブつけてね")]
    GameObject SBEff;

    [SerializeField, Header("MeleeUpEffectアタッチ")]
    GameObject NMUEff;

    [SerializeField, Header("MeleeDownEffectアタッチ")]
    GameObject NMDEff;

    // 生成用
    GameObject sb;

    [SerializeField, Header("MoveControllerアタッチ")]
    MoveController mc;

    [SerializeField, Header("HitColliderアタッチ")]
    PlHitCol hc;

    [SerializeField, Header("左右")]
    public int piInputLR = 0;

    [SerializeField, Header("後退不可")]
    public bool notBack = false;

    [SerializeField]
    public List<bool> actSkill = new List<bool>();
    [SerializeField]
    public List<float> skillTime = new List<float>();
    [SerializeField]
    public List<float> coolTime = new List<float>();

    PlayerData pd;

    private void Start()
    {
        gameMng = GameObject.Find("GameManager").GetComponent<Mng_Game>();

        //mc = GetComponent<MoveController>();
        //hc = GetComponent<HitCollider>();

        for (int i = 0; i < (int)SkillData.ACT_DATA.Num; i++)
        {
            actSkill.Add(false);
            skillTime.Add(0);
            coolTime.Add(0);
        }

        sb = Instantiate(SBEff, transform.position, Quaternion.identity);
        sb.transform.parent = this.transform;
        sb.SetActive(false);

        pd = GetComponent<PlayerData>();
    }

    private void Update()
    {
        // 移動処理（横移動、ジャンプ）
        MoveInput();

        if (hc.barrier > 0)
        {
            sb.SetActive(true);
        }
        else
        {
            sb.SetActive(false);
        }

        // NomalMelee : P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (skillTime[(int)SkillData.ACT_DATA.NM3] > 0)
            {

            }
            else if (skillTime[(int)SkillData.ACT_DATA.NM2] > 0)
            {
                if (actSkill[(int)SkillData.ACT_DATA.NM3] == false)
                {
                    ActNM(3);
                    SetSkill((int)SkillData.ACT_DATA.NM3, 0.5f, pd.NMCT);
                    gameMng.OneShotSE_C(SEData.Type.PlayerSE, Mng_Game.ClipSe.Atk1);
                }
            }
            else if (skillTime[(int)SkillData.ACT_DATA.NM1] > 0)
            {
                if (actSkill[(int)SkillData.ACT_DATA.NM2] == false)
                {
                    ActNM(2);
                    SetSkill((int)SkillData.ACT_DATA.NM2, 0.5f, pd.NMCT);
                    gameMng.OneShotSE_C(SEData.Type.PlayerSE, Mng_Game.ClipSe.Atk1);
                }
            }
            else if (skillTime[(int)SkillData.ACT_DATA.NM1] <= 0)
            {
                if (actSkill[(int)SkillData.ACT_DATA.NM1] == false && !CheckActSkill())
                {
                    ActNM(1);
                    SetSkill((int)SkillData.ACT_DATA.NM1, 0.5f, pd.NMCT);
                    gameMng.OneShotSE_C(SEData.Type.PlayerSE, Mng_Game.ClipSe.Atk1);
                }
            }
        }

        // NomalDodge : S or Shift
        //if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        //{
        //    if (actSkill[(int)SkillData.ACT_DATA.ND1] == false && !CheckActSkill() && pd.isND)
        //    {
        //        ActND();
        //        SetSkill((int)SkillData.ACT_DATA.ND1, 0.3f, 1);
        //    }
        //}

        // ノーマルドッジ、展示用
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (actSkill[(int)SkillData.ACT_DATA.ND1] == false && !CheckActSkill())
            {
                ActND();
                SetSkill((int)SkillData.ACT_DATA.ND1, 0.3f, 1);
            }
        }

        // LoveBeam : L
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (actSkill[(int)SkillData.ACT_DATA.LB1] == false && !CheckActSkill() && pd.isLB)
            {
                ActLB();
                SetSkill((int)SkillData.ACT_DATA.LB1, 1, 2);
            }
        }

        if (skillTime[(int)SkillData.ACT_DATA.LB1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);
        }

        // LoveMissile : M
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (actSkill[(int)SkillData.ACT_DATA.LM1] == false && !CheckActSkill() && pd.isLM)
            {
                ActLM(1);
                ActLM(2);
                ActLM(3);
                ActLM(4);
                ActLM(5);
                ActLM(6);
                SetSkill((int)SkillData.ACT_DATA.LM1, 0.5f, 2);
            }
        }

        if (skillTime[(int)SkillData.ACT_DATA.LM1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);
        }

        // AngerFire : F
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    if (actSkill[(int)SkillData.ACT_DATA.AF1] == false && !CheckActSkill() && pd.isAF)
        //    {
        //        ActAF(0);
        //        SetSkill((int)SkillData.ACT_DATA.AF1, 0.5f, 3);
        //    }
        //}

        // アンガーファイヤー、展示用
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (actSkill[(int)SkillData.ACT_DATA.AF1] == false && !CheckActSkill())
            {
                ActAF(0);
                SetSkill((int)SkillData.ACT_DATA.AF1, 0.5f, 3);
            }
        }

        if (skillTime[(int)SkillData.ACT_DATA.AF1] > 0 && pd.isAFUp)
        {
            mc.InputFlickStop();
            mc.InputLR(0);

            if (skillTime[(int)SkillData.ACT_DATA.AF1] <= 0.25f)
            {
                if (actSkill[(int)SkillData.ACT_DATA.AF2] == false)
                {
                    ActAF(1);
                    SetSkill((int)SkillData.ACT_DATA.AF2, 0, coolTime[(int)SkillData.ACT_DATA.AF1]);
                }
            }
        }

        // AngerCharge : C
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (actSkill[(int)SkillData.ACT_DATA.AC1] == false && !CheckActSkill() && pd.isAC)
            {
                ActAC();
                SetSkill((int)SkillData.ACT_DATA.AC1, 2, 3);
            }
        }

        if (skillTime[(int)SkillData.ACT_DATA.AC1] > 0)
        {
            mc.InputFlick((int)transform.localScale.x, 10, 0, false);
        }

        // AngerArea : O
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (actSkill[(int)SkillData.ACT_DATA.AA1] == false && !CheckActSkill() && pd.isAA)
            {
                ActAA();
                SetSkill((int)SkillData.ACT_DATA.AA1, 2, pd.AACT);
            }
        }

        if (skillTime[(int)SkillData.ACT_DATA.AA1] > 0)
        {
            mc.InputFlickStop();
            mc.InputLR(0);
        }

        // SorrowBarrier : B
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (actSkill[(int)SkillData.ACT_DATA.SB1] == false && !CheckActSkill() && pd.isSB)
            {
                ActSB();
                SetSkill((int)SkillData.ACT_DATA.SB1, 0.5f, pd.SBCT);
            }
        }

        if (skillTime[(int)SkillData.ACT_DATA.SB1] > 0)
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
    public bool CheckActSkill()
    {
        float totalTime = 0;

        for (int i = 0; i < (int)SkillData.ACT_DATA.Num; i++)
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
        if (!CheckActSkill())
        {
            // 左右入力：A and D
            if (Input.GetKey(KeyCode.D))
            {
                mc.InputLR(1);
                piInputLR = 1;
            }
            else if (Input.GetKey(KeyCode.A) && !notBack)
            {
                mc.InputLR(-1);
                piInputLR = -1;
            }
            else
            {
                mc.InputLR(0);
                piInputLR = 0;
            }
        }

        // キャラの向き変更
        if (mc.GetLR() != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * mc.GetLR();
            transform.localScale = scale;
        }

        // 上入力：W or space
        if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (mc.IsGround())
            {
                mc.InputJump();

                gameMng.OneShotSE_C(SEData.Type.PlayerSE, Mng_Game.ClipSe.Jump1);
            }
        }
    }

    void UpdateTime()
    {
        for (int i = 0; i < (int)SkillData.ACT_DATA.Num; i++)
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

    void SetSkill(int num, float st, float ct)
    {
        actSkill[num] = true;
        skillTime[num] = st;
        coolTime[num] = ct;
    }

    void ActNM(int rushNo)
    {
        // プレイヤー方向
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        // プレハブ生成
        GameObject obj = Instantiate(NMPre, plDir, Quaternion.identity);
        obj.transform.parent = this.transform;

        // エフェクト生成
        GameObject eff;
        Vector3 effDir = plDir;
        effDir.x -= transform.localScale.x / 2;

        if (rushNo % 2 == 1)
        {
            eff = Instantiate(NMUEff, effDir, Quaternion.identity);
        }
        else
        {
            eff = Instantiate(NMDEff, effDir, Quaternion.identity);
        }

        Vector3 effScale = eff.transform.localScale;
        effScale.x *= transform.localScale.x;
        eff.transform.localScale = effScale;

        eff.transform.parent = this.transform;

        // 大きさ変更
        Vector3 scale = obj.transform.localScale;
        scale.y = (rushNo + 1) * 0.5f;
        obj.transform.localScale = scale;

        // 位置変更
        plDir.y += 0.1f;
        if (rushNo == 3) { plDir.y += 1.5f; }

        // ダメージ変更
        float dmg = pd.nowAtk * (0.6f + 0.2f * rushNo);
        obj.GetComponent<AtkCol>().damage = (int)dmg;

        // 移動
        mc.InputFlick(plDir, (rushNo + 1) * 3.5f, 0.2f, true);
    }

    void ActND()
    {
        Vector3 plDir = this.transform.position;
        plDir.x += transform.localScale.x;

        mc.InputFlick(plDir, 20, 0.2f, true);
        hc.time = 0.3f;
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

        float delay;
        if (num == 1 || num == 6) { delay = 3; }
        else if (num == 2 || num == 5) { delay = 2; }
        else if (num == 3 || num == 4) { delay = 1; }
        else { delay = 0; }
        obj.GetComponent<MissileController>().delayCount = 0.1f * delay;

        if (transform.localScale.x <= 0)
        {
            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = 180 - (((float)num - 3.5f) *5);
            obj.transform.localEulerAngles = lea;
        }
        else
        {
            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = (((float)num - 3.5f)*5);
            obj.transform.localEulerAngles = lea;
        }
    }

    void ActAF(int num)
    {
        for (int i = 0; i < 6; i++)
        {
            GameObject obj = Instantiate(AFPre, transform.position, Quaternion.identity);

            Vector3 lea = obj.transform.localEulerAngles;
            lea.z = i * 60 + (num * 30);
            obj.transform.localEulerAngles = lea;

            float dmg = pd.nowAtk;
            obj.GetComponent<AtkCol>().damage = (int)dmg;
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
        hc.barrier = pd.SBValue;
    }
}
