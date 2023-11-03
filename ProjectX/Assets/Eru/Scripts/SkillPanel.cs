using UnityEngine;
using UnityEngine.UI;

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

    private Color arColor = new Color(255 / 255f, 100 / 255f, 100 / 255f, 255 / 255f);
    private Color srColor = new Color(100 / 255f, 100 / 255f, 255 / 255f, 255 / 255f);
    private Color lrColor = new Color(255 / 255f, 100 / 255f, 200 / 255f, 255 / 255f);

    private Color anColor = new Color(155 / 255f, 0 / 255f, 0 / 255f, 255 / 255f);
    private Color snColor = new Color(0 / 255f, 0 / 255f, 155 / 255f, 255 / 255f);
    private Color lnColor = new Color(255 / 255f, 80 / 255f, 90 / 255f, 255 / 255f);


    void Start()
    {
        image = GetComponent<Image>();
        releaseFlg = false;
        if ((stm.skillData & skillTree) == skillTree)
        {
            releaseFlg = true;
            if ((int)skillTree < 20000) image.color = arColor;
            else if((int)skillTree <= 1000000000) image.color = srColor;
            else image.color = lrColor;
        }
        else if (releaseConditions != 0 && (releaseConditions & stm.skillData) == 0
                || stm.angerFlg && skillTree == SkillTreeManager.SkillTree.empty2
                || stm.angerFlg && skillTree == SkillTreeManager.SkillTree.powerlessness
                || stm.sorrowFlg && skillTree == SkillTreeManager.SkillTree.angryPrincessTantrum
                || stm.sorrowFlg && skillTree == SkillTreeManager.SkillTree.swirlingEmotions)
        {
            if ((int)skillTree <= 20000) image.color = anColor;
            else if ((int)skillTree <= 1000000000) image.color = snColor;
            else image.color = lnColor;
        }
    }

    void Update()
    {
        if(stm.sorrowFlg && (int)skillTree >= 2000 && (int)skillTree <= 20000) image.color = anColor;
        else if(stm.angerFlg && (int)skillTree > 70000000 && (int)skillTree <= 1000000000) image.color = snColor;
        else if (releaseConditions == 0 && !releaseFlg
            || (releaseConditions & stm.skillData) != 0 && !releaseFlg)
        {
            if ((int)skillTree < 20000) image.color = Color.red;
            else if ((int)skillTree <= 1000000000) image.color = Color.blue;
        }
    }
}
