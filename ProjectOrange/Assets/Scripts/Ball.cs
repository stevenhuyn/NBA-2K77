using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float speed = 15;
    public Vector3? Target { get; set; }

    void Start() {
        Target = null;
    }

    void Update() {
        if (Target.HasValue) {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Target.Value, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<CharacterController>().GiveBall(this);
            ScoreSystem.UpdateScore(10);
            ScoreSystem.UpdateMultiplier(1);
        }
    }
}
