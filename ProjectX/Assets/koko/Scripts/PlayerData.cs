using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Eru;

public class PlayerData : MonoBehaviour
{
    public int plHp = 15;
    public int plAtk = 10;
    public int plDef = 0;

    public int nowAtk = 0;

    int beforHp = 0;
    float timer = 0;
    int addAtk = 0;
    int maxAtk = 5;

    public float NMCT = 1.4f;

    public bool isCH = false;

    public bool isAF = false;
    public bool isAFUp = false;
    public float AFCT = 15;

    public bool isAA = false;
    public float AACT = 25;

    public bool isAC = false;

    public bool isND = false;
    public bool isNDUp = false;

    public bool isSB = false;
    public float SBCT = 15;
    public int SBValue = 1;

    public bool isLM = false;
    public bool isLB = false;

    PlHitCol phc;

    private void Start()
    {
        // 嫌悪
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.disgust)
            == SkillTreeManager.SkillTree.disgust)
        {
            plAtk += 2;
        }

        // いらだち
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.irritation)
            == SkillTreeManager.SkillTree.irritation)
        {
            NMCT -= 0.2f;
        }

        // 嫉妬心
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.jealousy)
            == SkillTreeManager.SkillTree.jealousy)
        {
            NMCT -= 0.4f;
        }

        // 憤怒
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.anger)
            == SkillTreeManager.SkillTree.anger)
        {
            plAtk += 5;
        }

        // 恨み
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.resentment)
            == SkillTreeManager.SkillTree.resentment)
        {
            plAtk += 5;
        }

        // 激昂
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.rage)
            == SkillTreeManager.SkillTree.rage)
        {
            isAF = true;
        }

        // 憎しみの連鎖
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.chainofHatred)
            == SkillTreeManager.SkillTree.chainofHatred)
        {
            isCH = true;
        }

        // 燃え上がる怒り
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.burningAnger)
            == SkillTreeManager.SkillTree.burningAnger)
        {
            isAFUp = true;
        }

        // 反感
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.aversion)
            == SkillTreeManager.SkillTree.aversion)
        {
            maxAtk += 5;
        }

        // 欲求不満
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.frustration)
            == SkillTreeManager.SkillTree.frustration)
        {
            AACT -= 5;
        }

        // 闘争心
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.fightingSpirit)
            == SkillTreeManager.SkillTree.fightingSpirit)
        {
            AFCT -= 5;
        }

        // 怒れる姫の癇癪
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.angryPrincessTantrum)
            == SkillTreeManager.SkillTree.angryPrincessTantrum)
        {
            isAC = true;
        }

        // 渦巻いた感情
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.swirlingEmotions)
            == SkillTreeManager.SkillTree.swirlingEmotions)
        {
            isAA = true;
        }

        // 覚醒
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.awakening)
            == SkillTreeManager.SkillTree.awakening)
        {
            plAtk += 500;
        }

        // 紅ノ女王
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.birthoftheCrimsonQueen)
            == SkillTreeManager.SkillTree.birthoftheCrimsonQueen)
        {
            // だるいから実装したくねえ
        }

        // 悲惨
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.tragedy)
            == SkillTreeManager.SkillTree.tragedy)
        {
            plHp += 5;
        }

        // 恐怖からの逃亡
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.escapeFromFear)
            == SkillTreeManager.SkillTree.escapeFromFear)
        {
            isND = true;
        }

        // パニック
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.panic)
            == SkillTreeManager.SkillTree.panic)
        {
            plHp += 5;
        }

        // 緊張
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.tension)
            == SkillTreeManager.SkillTree.tension)
        {
            plDef += 1;
        }

        // 不安
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.anxiety)
            == SkillTreeManager.SkillTree.anxiety)
        {
            plHp += 5;
        }

        // 苦悩
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.suffering)
            == SkillTreeManager.SkillTree.suffering)
        {
            plDef += 2;
        }

        // 悲哀
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.grief)
            == SkillTreeManager.SkillTree.grief)
        {
            isNDUp = true;
        }

        // 命への失望
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.despairforLife)
            == SkillTreeManager.SkillTree.despairforLife)
        {
            isSB = true;
        }

        // 自暴自棄
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.recklessness)
            == SkillTreeManager.SkillTree.recklessness)
        {
            plHp += 10;
        }

        // 諦め
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.resignation)
            == SkillTreeManager.SkillTree.resignation)
        {
            plDef += 3;
        }

        // 
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.empty1)
            == SkillTreeManager.SkillTree.empty1)
        {

        }

        // 絶望
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.hopelessness)
            == SkillTreeManager.SkillTree.hopelessness)
        {
            SBValue += 1;
        }

        // 
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.empty2)
            == SkillTreeManager.SkillTree.empty2)
        {

        }

        // 無力
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.powerlessness)
            == SkillTreeManager.SkillTree.powerlessness)
        {
            // 実装しとうない
        }

        // 
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.empty3)
            == SkillTreeManager.SkillTree.empty3)
        {

        }

        // 愛
        if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.love)
            == SkillTreeManager.SkillTree.love)
        {
            isLB = true;
        }


        phc = transform.Find("PlHitCol").gameObject.GetComponent<PlHitCol>();

        beforHp = plHp;
        phc.maxHp = plHp;
        phc.nowHp = plHp;
    }

    private void Update()
    {
        nowAtk = plAtk + addAtk;

        if (isCH)
        {
            if (phc.nowHp != beforHp)
            {
                addAtk++;
                timer = 5;
                beforHp = phc.nowHp;
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                addAtk = 0;
            }
        }
    }
}
