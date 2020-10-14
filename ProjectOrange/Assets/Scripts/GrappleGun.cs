using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    public GameObject grapplingHookPrefab;

    void Update()
    {
        // Shoot in the direction the player is looking on left click
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GameObject hook = Instantiate(grapplingHookPrefab);
            hook.transform.position = transform.position;
            hook.transform.rotation = transform.rotation;
        }
    }
}
