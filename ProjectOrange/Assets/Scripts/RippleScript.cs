using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleScript : MonoBehaviour {
    public float amplitude = 0.05f, speed = 9.3f, wavelength = 27;
    public Color color = new Color(1, 0.59f, 0.2f);

    void Start() {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.shader = Shader.Find("Unlit/RippleShader");
        meshRenderer.material.SetFloat("_Amplitude", amplitude);
        meshRenderer.material.SetFloat("_Speed", speed);
        meshRenderer.material.SetFloat("_Wavelength", wavelength);
        meshRenderer.material.SetColor("_Color", color);
    }
}