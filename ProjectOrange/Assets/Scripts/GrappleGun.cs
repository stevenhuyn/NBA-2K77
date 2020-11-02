using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour {
    public GameObject grapplingHookPrefab;
    public float rotationSpeed = 2;
    public CharacterController Player { get; private set; }
    public GameObject Hook { get; private set; }

    private Quaternion targetRotation;

    void Start() {
        Player = GetComponentInParent<CharacterController>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Shoot a new Hook on left click
            DestroyHook();
            Hook = Instantiate(grapplingHookPrefab);
            Hook.GetComponent<GrapplingHook>().Gun = this;
            Hook.transform.position = Player.transform.position;
            Hook.transform.rotation = Camera.main.transform.rotation;
            Hook.transform.Rotate(Vector3.right, 90);
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            // Destroy the Hook if left click is released
            DestroyHook();
        }
        if (Hook) {
            targetRotation = Quaternion.LookRotation(Hook.transform.position - transform.position);
        } else {
            Player.GetComponent<Rigidbody>().useGravity = true;
            targetRotation = Camera.main.transform.rotation;
        }

        // Apply the rotation
        // Have to rotate 90 degrees to the right because we use UP us the main direction, not FORWARD
        transform.Rotate(-Vector3.right, 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        transform.Rotate(Vector3.right, 90);
    }

    public void DestroyHook() {
        if (Hook) {
            Destroy(Hook);
            Hook = null;
        }
    }
}
