using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {
    public float shootSpeed = 40, playerPullSpeed = 300;
    
    public CharacterController Player { get; set; }

    private bool stuck = false;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.up * shootSpeed, ForceMode.Impulse);
    }

    void Update() {
        if (stuck && Player) {
            // Pull on the player until they reach us
            Vector3 dir = (transform.position - Player.transform.position).normalized;
            Player.GetComponent<Rigidbody>().AddForce(dir * playerPullSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Surface")) {
            // Stick into the wall and begin pulling on the player
            stuck = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Player.GetComponent<Rigidbody>().useGravity = false;
        } else if (other.CompareTag("Ball")) {
            // Pull the ball towards the player
            other.GetComponent<Ball>().Target = Player.transform.position;
        } else if (Player && Player.gameObject == other.gameObject) {
            // Disappear when contacted by the player
            Destroy(gameObject);
        }
    }
}
