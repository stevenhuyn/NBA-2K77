using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    public float shootSpeed = 40, playerPullSpeed = 300;
    public GrappleGun Gun { get; set; }

    private bool stuck = false;
    private LineRenderer line = null;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
        line = gameObject.GetComponent<LineRenderer>();
    }

    void Update() {
        if (stuck) {
            // Pull on the player until they reach us
            Vector3 dir = (transform.position - Gun.Player.transform.position).normalized;
            Gun.Player.GetComponent<Rigidbody>().AddForce(dir * playerPullSpeed * Time.deltaTime);
        }

        // Draw the line to the gun
        line.SetPositions(new [] {transform.position, Gun.transform.position});
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Surface")) {
            // Stick into the wall and begin pulling on the player
            stuck = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Gun.Player.GetComponent<Rigidbody>().useGravity = false;
        } else if (other.CompareTag("Ball")) {
            // Pull the ball towards the player
            other.GetComponent<Ball>().Target = Gun.Player.transform.position;
        } else if (Gun.Player.gameObject == other.gameObject) {
            // Self-destruct when touched by the player
            Destroy(gameObject);
        }
    }
}
