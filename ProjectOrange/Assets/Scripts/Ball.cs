using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float speed = 15;
    public Vector3? Target { get; set; }
    public bool Moving { get; set; } // TODO: remove or add a check to GrapplingHook

    void Start() {
        Target = null;
        Moving = true;
    }

    void Update() {
        if (Moving && Target.HasValue) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Target.Value, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<CharacterController>().GiveBall(this);
        }
    }
}
