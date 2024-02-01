using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : MonoBehaviour
{
    [SerializeField]
    MoveController mc;

    private void Start()
    {
        mc = GetComponent<MoveController>();

    }

    private void Update()
    {
        if (mc.IsGround())
        {
            //mc.InputJump();
        }
    }

}
