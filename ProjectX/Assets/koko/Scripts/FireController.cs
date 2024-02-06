using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{

    [SerializeField]
    float spd = 10;

    private void FixedUpdate()
    {
        this.transform.Translate(Vector3.right * spd * Time.deltaTime);
    }
}
