using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Normal : MonoBehaviour
{
    Rigidbody rd;
    Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         force = new Vector3(-1f, 0f, 0f);
        rd.AddForce(force);
    }
}
