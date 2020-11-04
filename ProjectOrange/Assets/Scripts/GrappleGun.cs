using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleGun : MonoBehaviour {
    public GameObject grapplingHookPrefab;
    public float rotationSpeed = 2;
    public Color crosshairNormalColor = new Color(0.6352941f, 0.6352941f, 0.6352941f);
    public Color crosshairTargetingColor = new Color(0.9716981f, 0.3745972f, 0.06875221f);
    public float aimAssistSize = 10f;
    public float aimAssistMaxDegrees = 10f;
    
    public CharacterController Player { get; private set; }
    public GameObject Hook { get; private set; }

    private Image crosshairImage;
    private Quaternion targetRotation;

    void Start() {
        Player = GetComponentInParent<CharacterController>();
        crosshairImage = GameObject.Find("Crosshair").GetComponent<Image>();
    }

    void Update() {
        // Check if we're aiming at a ball or hoop
        RaycastHit? targetHit = null;
        var sphereHits = Physics.SphereCastAll(Player.transform.position, aimAssistSize, Camera.main.transform.forward);
        foreach (RaycastHit hit in sphereHits) {
            if (hit.transform.CompareTag("Ball") || HoopController.IsHoop(hit.transform.gameObject)) {
                // Rotate the maximum amount of degrees towards the target ball and check for line of sight
                Vector3 shiftedDir = Vector3.RotateTowards(
                    Camera.main.transform.forward,
                    hit.transform.position - Player.transform.position,
                    Mathf.Deg2Rad * aimAssistMaxDegrees,
                    0);
                RaycastHit checkHit;
                if (Physics.Raycast(Player.transform.position, shiftedDir, out checkHit)
                    && (checkHit.transform.CompareTag("Ball")
                        || HoopController.IsHoop(checkHit.transform.gameObject))) {
                    targetHit = hit;
                    break;
                }
            }
        }
        if (targetHit.HasValue) {
            crosshairImage.color = crosshairTargetingColor;
        } else {
            crosshairImage.color = crosshairNormalColor;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            // Shoot a new hook on left click
            DestroyHook();
            Hook = Instantiate(grapplingHookPrefab);
            Hook.GetComponent<GrapplingHook>().Gun = this;
            Hook.transform.position = Player.transform.position + Player.transform.forward * 0.5f;
            
            if (targetHit.HasValue) {
                // Use aim assist to aim towards the ball
                Hook.GetComponent<LineRenderer>().endColor = Color.red;
                Hook.transform.LookAt(targetHit.Value.transform);
                Hook.transform.Rotate(Vector3.right, 90);
            } else {
                // Aim normally
                Hook.transform.rotation = Camera.main.transform.rotation;
                Hook.transform.Rotate(Vector3.right, 90);
            }
        } else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            // Destroy the Hook if left click is released
            DestroyHook();
        }
        if (Hook) {
            targetRotation = Quaternion.LookRotation(Hook.transform.position - transform.position);
        } else {
            Player.GetComponent<Rigidbody>().useGravity = true;
            targetRotation = Camera.main.transform.rotation;
        }

        // Apply the rotation
        // Have to rotate 90 degrees to the right because we use UP us the main direction, not FORWARD
        transform.Rotate(-Vector3.right, 90);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        transform.Rotate(Vector3.right, 90);
    }

    public void DestroyHook() {
        if (Hook) {
            Destroy(Hook);
            Hook = null;
        }
    }
}
