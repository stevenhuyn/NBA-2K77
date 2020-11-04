using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopController : MonoBehaviour {
    public new ParticleSystem particleSystem;
    public AudioClip explosion;
    public bool disabled = false;

    public Material disabledMaterial;

    public void HandleDunk(List<Ball> balls) {
        ParticleSystem particles;
        Vector3 particlePosition = gameObject.transform.position;
        particles = Object.Instantiate(particleSystem, particlePosition, Quaternion.identity);
        particles.transform.parent = gameObject.transform.parent;
        //particles.transform.SetParent(gameObject.transform);
        particles.Play();

        AudioSource.PlayClipAtPoint(explosion, transform.position, 0.5f);
        DeactivateHoop();
    }

    void DeactivateHoop() {
        disabled = true;
        StartCoroutine(DelayDisableHoop());

    }

    IEnumerator DelayDisableHoop() {
        yield return new WaitForSeconds(1.5f);
        Transform torus = transform.Find("Torus");
        torus.GetComponent<Renderer>().material = disabledMaterial;
    }

    public static bool IsHoop(GameObject obj) {
        return obj && (obj.name == "Torus" || obj.name == "Hoop Inside");
    }
}
