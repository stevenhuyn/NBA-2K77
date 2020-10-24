using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Adapted from: https://github.com/jiankaiwang/FirstPersonController */

public class CharacterController : MonoBehaviour {
    public float jumpSpeed = 1.5f, ballHeightDiff = 0.8f;
    public Vector3 ballBottomPos = new Vector3(-0.8f, -0.5f, 0.7f);
    private bool grounded = false;

    private List<Ball> balls = new List<Ball>();
    private Rigidbody body;

    private float moveForce = 1.0f;
    public float groundSpeed = 1.0f, airSpeed = 0.1f;


    public float maxVelocity = 3.0f;
    public float brakeStrength = 1.0f;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        body = this.GetComponent<Rigidbody>();
    }

    void Update() {
        // Input.GetAxis() is used to get the user's input
        // You can further set it on Unity. (Edit, Project Settings, Input)
        Vector3 directionVector = new Vector3();
        directionVector.z = Input.GetAxis("Vertical") * moveForce * Time.deltaTime;
        directionVector.x = Input.GetAxis("Horizontal") * moveForce * Time.deltaTime;
        directionVector = Camera.main.transform.TransformDirection(directionVector);
        directionVector.y = 0;
        directionVector = Vector3.Normalize(directionVector);
        body.AddForce(moveForce * directionVector, ForceMode.VelocityChange);
        

        if (grounded) {
            capMovement();
            moveForce = groundSpeed;
        } else {
            moveForce = airSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            // Jump by adding force to the RigidBody (so we handle gravity)
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.AddForce(jumpSpeed * Vector3.up, ForceMode.Impulse);
        }
    }

    void FixedUpdate() {

    }

    void capMovement() {
        float speed = Vector3.Magnitude(body.velocity);

        if (speed > maxVelocity)
        {
            Debug.Log("Brake");
            float brakeSpeed = speed - maxVelocity;
            Vector3 horizontalVelocity = new Vector3 (body.velocity.x, 0, body.velocity.z);
            Vector3 normalizedVelocity = Vector3.Normalize(horizontalVelocity);
            Vector3 brakeVelocity = normalizedVelocity * brakeSpeed;
            body.AddForce(-brakeVelocity * brakeStrength, ForceMode.VelocityChange);
        }
    }

    /* Add a ball to the stack the player is holding.
     * Note: This is idempotent (won't add the same ball twice).
     */
    public void GiveBall(Ball ball) {
        if (!balls.Contains(ball)) {
            ball.transform.parent = transform;
            ball.Target = ballBottomPos + Vector3.up * balls.Count * ballHeightDiff;
            balls.Add(ball);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Ground") {
            grounded = true;
        }
        if (collision.gameObject.name == "Hoop") {
            HoopController hoop = collision.gameObject.GetComponent<HoopController>();
            if (balls.Count > 0) {
                grounded = false;
                hoop.HandleDunk(balls);
                ExplodeAwayFrom(hoop);
                ResetHeldBalls();
            }
        }
    }

    void OnCollisionExit(Collision collision){
        if (collision.gameObject.name == "Ground") {
            grounded = false;
        }
    }

    void ResetHeldBalls() {
        foreach(Ball ball in balls) {
            ball.reset();
        }
        balls.Clear();

    }

    void ExplodeAwayFrom(HoopController hoop) {
        Rigidbody body = this.gameObject.GetComponent<Rigidbody>();
        Vector3 hoopPosition = hoop.gameObject.transform.position;
        body.AddExplosionForce(300.0f, hoopPosition, 0.0f, 2.0f);
    }
}
