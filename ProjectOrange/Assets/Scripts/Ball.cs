using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float speed = 10f;
    public Vector3? Target { get; set; }

    void Start() {
        Target = null;
    }

    void Update() {
        if (Target.HasValue) {
            /*Vector3 dir = Target.Value - transform.localPosition;
            float step = Mathf.Min(speed * Time.deltaTime, dir.magnitude);
            transform.localPosition += step * dir.normalized;*/
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Target.Value, speed * Time.deltaTime);
        }
    }
}
