using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour {
    public new ParticleSystem particleSystem;

    public void HandleDunk(List<Ball> balls) {
        ParticleSystem particles;
        Vector3 particlePosition = gameObject.transform.position;
        particles = Object.Instantiate(particleSystem, particlePosition, Quaternion.identity);
        particles.transform.parent = gameObject.transform.parent;
        //particles.transform.SetParent(gameObject.transform);
        particles.Play();
    }
}
