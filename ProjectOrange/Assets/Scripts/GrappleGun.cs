using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour {
    public GameObject grapplingHookPrefab;

    private GameObject hook = null;

    void Update() {
        // On left click, either shoot a new hook or make the hook pull
        // (either pull the player to a wall or a ball to the player)
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (!hook) {
                // Shoot a new hook
                hook = Instantiate(grapplingHookPrefab);
                hook.transform.position = transform.position;
                hook.transform.rotation = transform.rotation;
            } else if (hook.GetComponent<GrapplingHook>().TryPull(this)) {
                // The hook successfully pulled and destroyed itself
                hook = null;
            }
        }
    }
}
