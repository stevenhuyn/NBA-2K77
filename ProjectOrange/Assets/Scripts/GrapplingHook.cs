using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float speed = 10;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * speed, ForceMode.Impulse);
    }
}
