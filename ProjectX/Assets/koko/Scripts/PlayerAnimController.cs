using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    enum AnimData
    {
        idol,
        run,
        attack1,
        attack2,
        attack3
    }

    [SerializeField, Header("amariアタッチ")]
    GameObject amari;

    [SerializeField, Header("amari_attack1アタッチ")]
    GameObject amari_Attack1;

    [SerializeField, Header("amari_attack2アタッチ")]
    GameObject amari_Attack2;

    [SerializeField, Header("amari_attack3アタッチ")]
    GameObject amari_Attack3;

    [SerializeField, Header("idol and run")]
    Animator amariAnim;

    [SerializeField, Header("atk1")]
    Animator atk1Anim;

    [SerializeField, Header("atk2")]
    Animator atk2Anim;

    [SerializeField, Header("atk3")]
    Animator atk3Anim;

    [SerializeField, Header("PlayerInputアタッチ")]
    PlayerInput pi;

    [SerializeField, Header("MoveControllerアタッチ")]
    MoveController mc;

    private void Start()
    {
        amariAnim = amari.GetComponent<Animator>();

        atk1Anim = amari_Attack1.GetComponent<Animator>();

        atk3Anim = amari_Attack3.GetComponent<Animator>();
    }

    private void Update()
    {
        //if (mc.GetLR() != 0)
        //{
        //    anim.Play("Amari_RunAnimation");
        //}
        //else
        //{
        //    anim.Play("Amari_NomalAnimation");
        //}
    }
}
