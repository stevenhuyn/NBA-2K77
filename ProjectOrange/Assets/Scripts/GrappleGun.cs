using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour {
    public GameObject grapplingHookPrefab;

    private GameObject hook = null;
    private CharacterController player = null;

    void Start() {
        player = GetComponentInParent<CharacterController>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Shoot a new hook on left click
            hook = Instantiate(grapplingHookPrefab);
            hook.GetComponent<GrapplingHook>().Player = player;
            hook.transform.position = transform.position;
            hook.transform.rotation = transform.rotation;
        } else if (hook && Input.GetKeyUp(KeyCode.Mouse0)) {
            // Destroy the hook if left click is released
            Destroy(hook);
            hook = null;
        }
        if (!hook) {
            player.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
