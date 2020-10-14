using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    public GameObject grapplingHookPrefab;
    public float pullSpeed = 300, upwardTilt = 5;

    private GameObject hook = null;

    void Update()
    {
        // On left click, either shoot a new hook or pull the player towards
        // an existing hook stuck in a wall
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (!hook) {
                // Shoot a new hook
                hook = Instantiate(grapplingHookPrefab);
                hook.transform.position = transform.position;
                hook.transform.rotation = transform.rotation;
            } else if (hook.GetComponent<GrapplingHook>().InWall) {
                // Pull the player towards the hook
                var player = GetComponentInParent<CharacterController>();
                Vector3 dir = (hook.transform.position + upwardTilt * Vector3.up - player.transform.position).normalized;
                player.GetComponent<Rigidbody>().AddForce(dir * pullSpeed);

                Destroy(hook);
                hook = null;
            }
        }
    }
}
