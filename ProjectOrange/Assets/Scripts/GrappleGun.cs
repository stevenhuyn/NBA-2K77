using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour {
    public GameObject grapplingHookPrefab;
    public CharacterController Player { get; private set; }
    public GameObject Hook { get; private set; }

    void Start() {
        Player = GetComponentInParent<CharacterController>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Shoot a new Hook on left click
            Hook = Instantiate(grapplingHookPrefab);
            Hook.GetComponent<GrapplingHook>().Gun = this;
            Hook.transform.position = Player.transform.position;
            Hook.transform.rotation = transform.rotation;
        } else if (Hook && Input.GetKeyUp(KeyCode.Mouse0)) {
            // Destroy the Hook if left click is released
            Destroy(Hook);
            Hook = null;
        }
        if (!Hook) {
            Player.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
