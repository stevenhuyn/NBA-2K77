using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public float amplitude = 0.05f;
    public float speed = 9.3f;
    public float waveLength = 27;
    public Color color = Color.gray;

    private Light pointLight;

    void Start() {
        // TODO: fix this, shouldn't work as we're using a Directional Light
        pointLight = GameObject.Find("Directional Light").GetComponent<Light>();
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material.shader = Shader.Find("Unlit/WaveShader");
        meshRenderer.material.SetColor("_PointLightColor", pointLight.color);
        meshRenderer.material.SetVector("_PointLightPosition", pointLight.transform.position);
        meshRenderer.material.SetFloat("_Amplitude", amplitude);
        meshRenderer.material.SetFloat("_Speed", speed);
        meshRenderer.material.SetFloat("_Wavelength", waveLength);
    }
}