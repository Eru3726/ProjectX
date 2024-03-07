using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmBulletController : MonoBehaviour
{
    MoveController mc;

    GameObject player;

    private void Start()
    {
        mc = GetComponent<MoveController>();

        player = GameObject.Find("Player");

        int plDir = (player.transform.position.x > transform.position.x ? 1 : -1);

        mc.InputFlick(new Vector3(transform.position.x + plDir, transform.position.y + 1, transform.position.z),10, 0.5f);
    }
}
