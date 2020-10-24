using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public ParticleSystem particleSystem;

    public void HandleDunk(List<Ball> balls) {
        ParticleSystem particles;
        particles = Object.Instantiate(particleSystem, gameObject.transform.position, Quaternion.identity);
        particles.transform.SetParent(transform);
        particles.Play();
    }

}
