using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Adapted from: https://github.com/jiankaiwang/FirstPersonController */

public class CharacterController : MonoBehaviour {
    public float speed = 10.0f, ballHeightDiff = 1;
    public Vector3 ballBottomPos = new Vector3(-10, -1, 1);
    private float translation, strafe;
    private List<Ball> balls = new List<Ball>();

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        // Input.GetAxis() is used to get the user's input
        // You can further set it on Unity. (Edit, Project Settings, Input)
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        strafe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape")) {
            // turn on the cursor
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void GiveBall(Ball ball) {
        ball.transform.parent = transform;
        ball.transform.localPosition = ballBottomPos + Vector3.up * balls.Count * ballHeightDiff;
        ball.Moving = false;
        balls.Add(ball);
    }
}
