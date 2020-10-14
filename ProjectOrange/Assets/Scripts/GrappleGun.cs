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
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Shoot a new hook, discarding any previous hook
            if (hook) {
                Destroy(hook);
            }
            hook = Instantiate(grapplingHookPrefab);
            hook.transform.position = transform.position;
            hook.transform.rotation = transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && hook && hook.GetComponent<GrapplingHook>().InWall) {
            // Pull the player towards the last shot hook
            var player = GetComponentInParent<CharacterController>();
            Vector3 dir = (hook.transform.position + upwardTilt * Vector3.up - player.transform.position).normalized;
            player.GetComponent<Rigidbody>().AddForce(dir * pullSpeed);

            Destroy(hook);
            hook = null;
        }
    }
}
