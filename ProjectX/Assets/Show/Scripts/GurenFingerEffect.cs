using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GurenFingerEffect : MonoBehaviour
{
    GameObject player;
    float distance = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 dis = player.transform.position - transform.position;

        //if (dis.x >= -distance)
        //{
        //    transform.localScale = new Vector3(-0.2f, 0.2f, 1);
        //}
        //if (dis.x <= distance)
        //{
        //    transform.localScale = new Vector3(-0.2f, 0.2f, 1);
        //}
        //Destroy(gameObject, 0.1f);
    }
}
