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
            if (hit.transform.CompareTag("Ball")) {
                // Rotate the maximum amount of degrees towards the target ball and check for line of sight
                Vector3 shiftedDir = Vector3.RotateTowards(transform.up, hit.transform.position - Player.transform.position, Mathf.Deg2Rad * aimAssistMaxDegrees, 0);
                RaycastHit checkHit;
                if (Physics.Raycast(Player.transform.position, shiftedDir, out checkHit)
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
                // Use aim assist to aim towards the ball
                Hook.GetComponent<LineRenderer>().endColor = Color.red;
                Hook.transform.LookAt(hitBall.Value.transform);
                Hook.transform.Rotate(Vector3.right, 90);
            } else {
                // Aim normally
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
