using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{

    [SerializeField, Header("amariアニメアタッチ")]
    GameObject amari;

    [SerializeField, Header("Playerアタッチ")]
    GameObject parentPlayer;

    [SerializeField]
    Animator anim;

    [SerializeField]
    Animator animRun;

    [SerializeField]
    PlayerInput pi;

    [SerializeField]
    MoveController mc;

    [SerializeField]
    Rigidbody2D rb2d;

    private void Start()
    {
        anim = amari.GetComponent<Animator>();

        pi = parentPlayer.GetComponent<PlayerInput>();
        mc = parentPlayer.GetComponent<MoveController>();
        rb2d = parentPlayer.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (pi.CheckActSkill())
        {
            if (pi.skillTime[(int)StageData.ACT_DATA.NM3] > 0)
            {
                anim.Play("Amari_Attack3Animation_Final");
            }
            else if (pi.skillTime[(int)StageData.ACT_DATA.NM2] > 0)
            {
                anim.Play("Amari_Attack2Animation_Final");
            }
            else if (pi.skillTime[(int)StageData.ACT_DATA.NM1] > 0)
            {
                anim.Play("Amari_Attack1Animation_Final");
            }
            else if (pi.skillTime[(int)StageData.ACT_DATA.LB1] > 0 || pi.skillTime[(int)StageData.ACT_DATA.LM1] > 0)
            {
                anim.Play("Amari_BeamPAnimation_Final");
            }
            else if (pi.skillTime[(int)StageData.ACT_DATA.AA1] > 0 || pi.skillTime[(int)StageData.ACT_DATA.AF1] > 0)
            {
                anim.Play("Amari_BeamRAnimation_Final");
            }
            else if (pi.skillTime[(int)StageData.ACT_DATA.ND1] > 0)
            {
                anim.Play("Amari_AvoidanceAnimation");
            }
        }
        else if(!mc.IsGround())
        {
            if(rb2d.velocity.y >= 0)
            {
                anim.Play("Amari_JumpAnimation_Final");
            }
            else
            {
                anim.Play("Amari_FallingAnimation_Final");
            }
        }
        else
        {
            if (mc.GetLR() != 0)
            {
                anim.Play("Amari_RunAnimation_Final");
            }
            else
            {
                anim.Play("Amari_IdleAnimation_Final");
            }
        }
    }
}
