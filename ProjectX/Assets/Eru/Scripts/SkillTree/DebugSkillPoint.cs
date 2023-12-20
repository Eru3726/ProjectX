using UnityEngine;
using UnityEngine.UI;
using Eru;

namespace EruDebug
{
    public class DebugSkillPoint : MonoBehaviour
    {
        private Text skillPointText;

        void Start()
        {
            skillPointText = GetComponent<Text>();
        }

        void Update()
        {
            skillPointText.text = "SP:" +  SkillTreeManager.skillPoint.ToString();
        }

        public void PointUp(int value)
        {
            SkillTreeManager.skillPoint += value;
        }

        public void PointDown(int value)
        {
            SkillTreeManager.skillPoint -= value;
        }

        public void SkillConfirmation()
        {
            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.disgust)
                == SkillTreeManager.SkillTree.disgust) Debug.Log("嫌悪解放済み");
            //else Debug.Log("嫌悪未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.irritation)
                == SkillTreeManager.SkillTree.irritation) Debug.Log("いらだち解放済み");
            //else Debug.Log("いらだち未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.jealousy)
                == SkillTreeManager.SkillTree.jealousy) Debug.Log("嫉妬心解放済み");
            //else Debug.Log("嫉妬心未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.anger)
                == SkillTreeManager.SkillTree.anger) Debug.Log("憤怒解放済み");
            //else Debug.Log("憤怒未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.resentment)
                == SkillTreeManager.SkillTree.resentment) Debug.Log("恨み解放済み");
            //else Debug.Log("恨み未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.rage)
                == SkillTreeManager.SkillTree.rage) Debug.Log("激昂解放済み");
            //else Debug.Log("激昂未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.chainofHatred)
                == SkillTreeManager.SkillTree.chainofHatred) Debug.Log("憎しみの連鎖解放済み");
            //else Debug.Log("憎しみの連鎖未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.burningAnger)
                == SkillTreeManager.SkillTree.burningAnger) Debug.Log("燃え上がる怒り解放済み");
            //else Debug.Log("燃え上がる怒り未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.aversion)
                == SkillTreeManager.SkillTree.aversion) Debug.Log("反感解放済み");
            //else Debug.Log("反感未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.frustration)
                == SkillTreeManager.SkillTree.frustration) Debug.Log("欲求不満解放済み");
            //else Debug.Log("欲求不満未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.fightingSpirit)
                == SkillTreeManager.SkillTree.fightingSpirit) Debug.Log("闘争心解放済み");
            //else Debug.Log("闘争心未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.angryPrincessTantrum)
                == SkillTreeManager.SkillTree.angryPrincessTantrum) Debug.Log("怒れる姫の癇癪解放済み");
            //else Debug.Log("怒れる姫の癇癪未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.swirlingEmotions)
                == SkillTreeManager.SkillTree.swirlingEmotions) Debug.Log("渦巻いた感情解放済み");
            //else Debug.Log("渦巻いた感情未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.awakening)
                == SkillTreeManager.SkillTree.awakening) Debug.Log("覚醒解放済み");
            //else Debug.Log("覚醒未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.birthoftheCrimsonQueen)
                == SkillTreeManager.SkillTree.birthoftheCrimsonQueen) Debug.Log("紅ノ女王誕生解放済み");
            //else Debug.Log("紅ノ女王誕生未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.tragedy)
                == SkillTreeManager.SkillTree.tragedy) Debug.Log("悲惨解放済み");
            //else Debug.Log("悲惨未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.escapeFromFear)
                == SkillTreeManager.SkillTree.escapeFromFear) Debug.Log("恐怖からの逃亡解放済み");
            //else Debug.Log("恐怖からの逃亡未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.panic)
                == SkillTreeManager.SkillTree.panic) Debug.Log("パニック解放済み");
            //else Debug.Log("パニック未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.tension)
                == SkillTreeManager.SkillTree.tension) Debug.Log("緊張解放済み");
            //else Debug.Log("緊張未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.anxiety)
                == SkillTreeManager.SkillTree.anxiety) Debug.Log("不安解放済み");
            //else Debug.Log("不安未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.suffering)
                == SkillTreeManager.SkillTree.suffering) Debug.Log("苦悩解放済み");
            //else Debug.Log("苦悩未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.grief)
                == SkillTreeManager.SkillTree.grief) Debug.Log("悲哀解放済み");
            //else Debug.Log("悲哀未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.despairforLife)
                == SkillTreeManager.SkillTree.despairforLife) Debug.Log("命への失望解放済み");
            //else Debug.Log("命への失望未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.recklessness)
                == SkillTreeManager.SkillTree.recklessness) Debug.Log("自暴自棄解放済み");
            //else Debug.Log("自暴自棄未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.resignation)
                == SkillTreeManager.SkillTree.resignation) Debug.Log("諦め解放済み");
            //else Debug.Log("諦め未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.empty1)
                == SkillTreeManager.SkillTree.empty1) Debug.Log("Enpty1解放済み");
            //else Debug.Log("Enpty1未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.hopelessness)
                == SkillTreeManager.SkillTree.hopelessness) Debug.Log("絶望解放済み");
            //else Debug.Log("絶望未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.empty2)
                == SkillTreeManager.SkillTree.empty2) Debug.Log("Enpty2解放済み");
            //else Debug.Log("Enpty2未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.powerlessness)
                == SkillTreeManager.SkillTree.powerlessness) Debug.Log("無力解放済み");
            //else Debug.Log("無力未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.empty3)
                == SkillTreeManager.SkillTree.empty3) Debug.Log("Enpty3解放済み");
            //else Debug.Log("Enpty3未開放");

            if ((SkillTreeManager.skillData & SkillTreeManager.SkillTree.love)
                == SkillTreeManager.SkillTree.love) Debug.Log("愛解放済み");
            //else Debug.Log("愛未開放");
        }
    }
}