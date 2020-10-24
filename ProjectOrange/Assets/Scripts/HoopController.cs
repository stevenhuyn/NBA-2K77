using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour {
    public new ParticleSystem particleSystem;
    public AudioSource audioSource;
    public AudioClip explosion;
    public void HandleDunk(List<Ball> balls) {
        ParticleSystem particles;
        particles = Object.Instantiate(particleSystem, gameObject.transform.position, Quaternion.identity);
        particles.transform.SetParent(transform);
        particles.Play();

        AudioSource.PlayClipAtPoint(explosion, transform.position, 0.5f);
    }
}
