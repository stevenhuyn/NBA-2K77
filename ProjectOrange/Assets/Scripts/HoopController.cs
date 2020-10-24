using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleDunk(List<Ball> balls) {
        ParticleSystem particles;
        particles = Object.Instantiate(particleSystem, gameObject.transform.position, Quaternion.identity);
        particles.transform.SetParent(transform);
        particles.Play();
    }

}
