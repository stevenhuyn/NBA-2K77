using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleGun : MonoBehaviour {
    public GameObject grapplingHookPrefab;
    public Color crosshairNormalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f);
    public Color crosshairTargetingColor = new Color(0.9716981f, 0.3745972f, 0.06875221f);
    public float aimAssistSize = 10f;
    public float aimAssistMaxDegrees = 10f;
    public CharacterController Player { get; private set; }
    public GameObject Hook { get; private set; }

    private Image crosshairImage;

    void Start() {
        Player = GetComponentInParent<CharacterController>();
        crosshairImage = GameObject.Find("Crosshair").GetComponent<Image>();
    }

    void Update() {
        // Check if we're aiming at a ball
        RaycastHit? hitBall = null;
        foreach (RaycastHit hit in Physics.SphereCastAll(Player.transform.position, aimAssistSize, transform.up)) {
            if (hit.transform.CompareTag("Ball")
                && Vector3.Angle(transform.up, hit.transform.position - Player.transform.position) <= aimAssistMaxDegrees) {
                // Make sure we actually have a straight line of sight to the ball
                RaycastHit checkHit;
                if (Physics.Raycast(Player.transform.position, hit.transform.position - Player.transform.position, out checkHit)
                    && checkHit.transform.CompareTag("Ball")) {
                    hitBall = hit;
                    break;
                }
            }
        }
        if (hitBall.HasValue) {
            crosshairImage.color = crosshairTargetingColor;
        } else {
            crosshairImage.color = crosshairNormalColor;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Shoot a new hook on left click
            DestroyHook();
            Hook = Instantiate(grapplingHookPrefab);
            Hook.GetComponent<GrapplingHook>().Gun = this;
            Hook.transform.position = Player.transform.position;
            
            if (hitBall.HasValue) {
                // Aim towards the ball
                Hook.GetComponent<LineRenderer>().endColor = Color.red;
                Hook.transform.LookAt(hitBall.Value.transform);
                Hook.transform.Rotate(Vector3.right, 90);
            } else {
                Hook.transform.rotation = transform.rotation;
            }
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            // Destroy the Hook if left click is released
            DestroyHook();
        }
        if (!Hook) {
            Player.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    public void DestroyHook() {
        if (Hook) {
            Destroy(Hook);
            Hook = null;
        }
    }
}
