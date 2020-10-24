using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    public float shootSpeed = 40, playerPullSpeed = 20, ballPullSpeed = 0.4f, ballAcceleration = 0.01f;
    public GrappleGun Gun { get; set; }
    public bool Stuck { get; private set; }

    private float ballSpeed;
    private Ball ball = null;
    private LineRenderer line = null;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
        line = gameObject.GetComponent<LineRenderer>();
        ballSpeed = ballPullSpeed;
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
                // Ball moves faster the longer you hold onto it
                ballSpeed += ballAcceleration;
                // Move back towards the player, pulling on the ball
                ball.transform.position = Vector3.MoveTowards(ball.transform.position, Gun.Player.transform.position, ballSpeed);
                transform.position = Vector3.MoveTowards(transform.position, Gun.Player.transform.position, ballSpeed);
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
                Ball ball = other.GetComponent<Ball>();
                if (!ball.Target.HasValue) {
                    // Only stick into balls that have not been picked up yet
                    this.ball = ball;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
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
