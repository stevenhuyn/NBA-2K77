using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    public float shootSpeed = 40, playerPullSpeed = 20, ballPullSpeed = 0.4f;
    public GrappleGun Gun { get; set; }
    public bool Stuck { get; private set; }

    private Ball ball = null;
    private LineRenderer line = null;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
        line = gameObject.GetComponent<LineRenderer>();
    }

    void FixedUpdate() {
        if (Stuck) {
            // Pull on the player until they reach us
            Vector3 dir = (transform.position - Gun.Player.transform.position).normalized;
            Gun.Player.GetComponent<Rigidbody>().AddForce(dir * playerPullSpeed);
        } else if (ball) {
            if (ball.Target.HasValue) {
                // Ball has been picked up by the player
                Destroy(gameObject);
            } else {
                // Move back towards the player, pulling on the ball
                Vector3 velocity = ballPullSpeed * (Gun.Player.transform.position - transform.position).normalized;
                transform.position += velocity;
                ball.transform.position += velocity;
            }
        }

        // Draw the line to the gun
        line.SetPositions(new [] {transform.position, Gun.transform.position});
    }

    void OnTriggerEnter(Collider other) {
        if (!Stuck && !ball) {
            if (other.CompareTag("Surface")) {
                // Stick into the wall and begin pulling on the player
                Stuck = true;
                Destroy(GetComponent<Rigidbody>());
                Gun.Player.GetComponent<Rigidbody>().useGravity = false;
            } else if (other.CompareTag("Ball")) {
                // Pull the ball towards the player
                ball = other.GetComponent<Ball>();
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        } else if (Gun.Player.gameObject == other.gameObject) {
            // Self-destruct when touched by the player
            Destroy(gameObject);
            if (ball) {
                Gun.Player.GiveBall(ball);
            }
        }
    }
}
