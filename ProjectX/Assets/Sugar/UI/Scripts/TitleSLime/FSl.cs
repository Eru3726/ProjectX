using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSl : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(initialPosition.x, Mathf.Sin(Time.time) + initialPosition.y, initialPosition.z);
    }
}
