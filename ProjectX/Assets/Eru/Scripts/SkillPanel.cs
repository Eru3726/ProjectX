using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
