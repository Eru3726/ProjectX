using UnityEngine;
using UnityEngine.UI;

namespace Eru
{
    public class LineManager : MonoBehaviour
    {
        [SerializeField, Header("スキル解放条件")]
        private SkillTreeManager.SkillTree releaseConditions;

        [SerializeField, Header("未解放画像")]
        private Sprite unreleasedSprite;

        [SerializeField, Header("解放済み画像")]
        private Sprite releasableSprite;

        private Image image;

        void Start()
        {
            image = GetComponent<Image>();
        }

        void Update()
        {
            if (releaseConditions == 0 ||
               (releaseConditions & SkillTreeManager.skillData) != 0) image.sprite = releasableSprite;
            else image.sprite = unreleasedSprite;
        }
    }
}