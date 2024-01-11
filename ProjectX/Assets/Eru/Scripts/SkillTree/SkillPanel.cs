using UnityEngine;
using UnityEngine.UI;

namespace Eru
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class SkillPanel : MonoBehaviour
    {
        [Header("必要なスキルポイント")]
        public int requiredSP;

        [Header("スキル")]
        public SkillTreeManager.SkillTree skillTree;

        [Header("スキル名")]
        public string skillName;

        [Header("効果説明"), Multiline]
        public string explanation;

        [Header("スキル解放条件")]
        public SkillTreeManager.SkillTree releaseConditions;

        [SerializeField]
        private SkillTreeManager stm;

        [HideInInspector]
        public Image image;

        [HideInInspector]
        public bool releaseFlg = false;

        [SerializeField, Header("解放済み画像")]
        private Sprite releasedSprite;

        [SerializeField, Header("解放可能画像")]
        private Sprite releasableSprite;

        [SerializeField,Header("ロック画像")]
        private Sprite lockSprite;


        void Start()
        {
            image = GetComponent<Image>();
            releaseFlg = false;
            if ((SkillTreeManager.skillData & skillTree) == skillTree)
            {
                releaseFlg = true;
                image.sprite = releasedSprite;
            }
            else if (releaseConditions != 0 && (releaseConditions & SkillTreeManager.skillData) == 0
                    || stm.angerFlg && skillTree == SkillTreeManager.SkillTree.empty2
                    || stm.angerFlg && skillTree == SkillTreeManager.SkillTree.powerlessness
                    || stm.sorrowFlg && skillTree == SkillTreeManager.SkillTree.angryPrincessTantrum
                    || stm.sorrowFlg && skillTree == SkillTreeManager.SkillTree.swirlingEmotions) image.sprite = lockSprite;
        }

        void Update()
        {
            if (stm.sorrowFlg && (int)skillTree >= 2000 && (int)skillTree <= 20000 ||
                stm.angerFlg && (int)skillTree > 70000000 && (int)skillTree <= 1000000000) image.sprite = lockSprite;
            else if (releaseConditions == 0 && !releaseFlg
                || (releaseConditions & SkillTreeManager.skillData) != 0 && !releaseFlg) image.sprite = releasableSprite;
        }
    }
}