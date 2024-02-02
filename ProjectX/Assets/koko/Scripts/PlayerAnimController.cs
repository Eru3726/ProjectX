using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    GameObject pa;
    [SerializeField]
    MoveController mc;

    private void Start()
    {
        anim = GetComponent<Animator>();

        pa = transform.parent.gameObject;

        mc = pa.GetComponent<MoveController>();
    }

    private void Update()
    {
        if (mc.GetLR() != 0)
        {
            anim.Play("Amari_RunAnimation");
        }
        else
        {
            anim.Play("Amari_NomalAnimation");
        }
    }
}
