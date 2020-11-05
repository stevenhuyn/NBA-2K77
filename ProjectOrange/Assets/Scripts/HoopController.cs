﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour {
    public new ParticleSystem particleSystem;
    public ParticleSystem disabledParticleSystem;
    public AudioClip explosion;
    public bool disabled = false;

    public Material disabledMaterial;

    public void HandleDunk(List<Ball> balls) {
        PlayParticles();
        AudioSource.PlayClipAtPoint(explosion, transform.position, 0.5f);
        DeactivateHoop();
    }

    public void PlayParticles() {
        ParticleSystem effect = disabled ? disabledParticleSystem : particleSystem;
        Vector3 particlePosition = gameObject.transform.position;
        ParticleSystem instance = Object.Instantiate(effect, particlePosition, Quaternion.identity);
        instance.transform.parent = gameObject.transform.parent;
        instance.Play();
    }

    void DeactivateHoop() {
        disabled = true;
        StartCoroutine(DelayDisableHoop());
    }

    IEnumerator DelayDisableHoop() {
        yield return new WaitForSeconds(1.5f);
        Transform torus = transform.Find("Torus");
        torus.GetComponent<Renderer>().material = disabledMaterial;
        var hoopWave = transform.Find("Hoop Inside").transform.Find("Hoop Wave").GetComponent<RippleScript>();
        hoopWave.color = new Color(0, 0.66f, 0.66f);
        hoopWave.UpdateShader();
    }

    public static bool IsHoop(GameObject obj) {
        return obj && (obj.name == "Torus" || obj.name == "Hoop Inside");
    }
}
