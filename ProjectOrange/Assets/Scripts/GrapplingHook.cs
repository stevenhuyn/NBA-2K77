using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    public float shootSpeed = 40, playerPullSpeed = 20, ballPullSpeed = 0.4f, ballAcceleration = 0.01f;
    public GrappleGun Gun { get; set; }
    public bool Stuck { get; private set; }

    public CharacterController character;

    private float ballSpeed;
    private Ball ball = null;
    private LineRenderer line = null;

    private Camera playerCamera;
    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
        line = gameObject.GetComponent<LineRenderer>();
        ballSpeed = ballPullSpeed;

        playerCamera = character.transform.GetComponentInChildren<Camera>();
    }

    void Update() {
        // Draw the line to the gun
        int lineSegments = 1000;
        Vector3[] pathNodes = new Vector3[lineSegments + 1];
        line.positionCount = lineSegments + 1;

        Vector3 direction = transform.position - Gun.transform.position;
        for (int i = 0; i <= lineSegments; i++) {
            pathNodes[i] = Gun.transform.position + (direction * i/lineSegments);
        }
        line.SetPositions(pathNodes);


        //Update reference vectors
        Vector4 up = Camera.main.transform.up;
        line.material.SetVector("_Up", up);
        line.material.SetVector("_GunLocation", Gun.transform.position);
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
