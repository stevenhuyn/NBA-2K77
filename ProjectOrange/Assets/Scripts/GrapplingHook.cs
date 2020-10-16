using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float shootSpeed = 40;
    public bool InWall { get; private set; }

    // TODO: need to make the hook disappear if it falls into the void or something
    // (you can't shoot again until it hits the wall)
    // Another option: allow multiple hooks to exist at once but only allowing
    // pulling towards the most recently fired one

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider other) {
        // Stick in the wall
        InWall = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
