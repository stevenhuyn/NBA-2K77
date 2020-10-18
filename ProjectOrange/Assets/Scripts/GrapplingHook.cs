using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    public float shootSpeed, playerPullSpeed, upwardTilt;
    
    private bool stuck = false;
    private GameObject ball = null;

    // TODO: need to make the hook disappear if it falls into the void or something
    // (you can't shoot again until it hits the wall)
    // Another option: allow multiple hooks to exist at once but only allowing
    // pulling towards the most recently fired one

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
    }


    private void BecomeStuck() {
        stuck = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Surface")) {
            BecomeStuck();
        } else if (other.CompareTag("Ball")) {
            BecomeStuck();
            ball = other.gameObject;
        }
    }

    /* Attempt to either pull the player holding the given grappling gun to a
     * wall, or a caught ball towards that same player. If the pull is
     * succesfuly, self-destruct and return true. Otherwise do nothing and
     * return false.
     */
    public bool TryPull(GrappleGun gun) {
        if (stuck) {
            var player = gun.GetComponentInParent<CharacterController>();
            if (ball) {
                // Stuck in a ball: pull the ball towards the player
                ball.GetComponent<Ball>().Target = player.transform.position;
            } else {
                // Stuck in a non-ball surface: pull the player towards the hook
                Vector3 dir = (transform.position + upwardTilt * Vector3.up - player.transform.position).normalized;
                player.GetComponent<Rigidbody>().AddForce(dir * playerPullSpeed);
            }
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
