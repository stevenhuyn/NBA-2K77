using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Adapted from: https://github.com/jiankaiwang/FirstPersonController */

public class CharacterController : MonoBehaviour {
    public float speed = 10, jumpSpeed = 1.5f, ballHeightDiff = 0.8f;
    public Vector3 ballBottomPos = new Vector3(-0.8f, -0.5f, 0.7f);
    public bool grounded = false;
    private List<Ball> balls = new List<Ball>();

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Input.GetAxis() is used to get the user's input
        // You can further set it on Unity. (Edit, Project Settings, Input)
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(strafe, 0, translation);
        // FIXME: ^ is it ok to be modifying the transform directly here
        // when we have a RigidBody?

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
            ScoreSystem.ResetMultiplier();
        }
    }

    void OnCollisionExit(Collision collision){
        if (collision.gameObject.name == "Ground") {
            grounded = false;

        }
    }
}
