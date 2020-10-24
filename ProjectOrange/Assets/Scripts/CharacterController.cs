using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Adapted from: https://github.com/jiankaiwang/FirstPersonController */

public class CharacterController : MonoBehaviour {
    public float
        jumpSpeed = 1.5f, ballHeightDiff = 0.8f,
        groundSpeed = 8, airSpeed = 1.5f,
        maxGroundSpeed = 10, maxAirSpeed = 50,
        brakeStrength = 5,
        groundDrag = 3, airDrag = 0;
    public Vector3 ballBottomPos = new Vector3(-0.8f, -0.5f, 0.7f);

    private bool grounded = false;
    private List<Ball> balls = new List<Ball>();
    private new Rigidbody rigidbody;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            // Jump by adding force to the Rigidbody (so we handle gravity)
            rigidbody.AddForce(jumpSpeed * Vector3.up, ForceMode.Impulse);
        }
    }

    void FixedUpdate() {
        float moveSpeed;
        if (grounded) {
            moveSpeed = groundSpeed;
            rigidbody.drag = groundDrag;
        } else {
            moveSpeed = airSpeed;
            rigidbody.drag = airDrag;
        }

        // Calculate unit vector of desired direction in the XZ plane
        Vector3 directionVector = new Vector3();
        directionVector.z = Input.GetAxis("Vertical") * moveSpeed;
        directionVector.x = Input.GetAxis("Horizontal") * moveSpeed;
        directionVector = Camera.main.transform.TransformDirection(directionVector);
        directionVector.y = 0;
        directionVector.Normalize();

        rigidbody.AddForce(moveSpeed * directionVector);

        // Cap movement speed
        float speed = rigidbody.velocity.magnitude;
        float maxSpeed = grounded ? maxGroundSpeed : maxAirSpeed;
        if (speed > maxSpeed) {
            // Apply force in reverse direction to slow player down
            float brakeSpeed = speed - maxSpeed;
            Vector3 horizontalDir = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z).normalized;
            rigidbody.AddForce(-horizontalDir * brakeSpeed * brakeStrength);
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
        foreach (Ball ball in balls) {
            ball.Reset();
        }
        balls.Clear();
    }

    void ExplodeAwayFrom(HoopController hoop) {
        Vector3 hoopPosition = hoop.gameObject.transform.position;
        rigidbody.AddExplosionForce(300.0f, hoopPosition, 0.0f, 2.0f);
    }
}
