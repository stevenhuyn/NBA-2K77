using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleScript : MonoBehaviour {
    public float amplitude = 0.05f, speed = 9.3f, wavelength = 27;
    public Color color = new Color(1, 0.59f, 0.2f);
    public float colorMinValue = 0.2f, colorMaxValue = 1.1f;

    void Start() {
        UpdateShader();
    }
    public void UpdateShader() {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.shader = Shader.Find("Unlit/RippleShader");
        meshRenderer.material.SetFloat("_Amplitude", amplitude);
        meshRenderer.material.SetFloat("_Speed", speed);
        meshRenderer.material.SetFloat("_Wavelength", wavelength);
        meshRenderer.material.SetColor("_Color", color);
        meshRenderer.material.SetFloat("_ColorMinValue", colorMinValue);
        meshRenderer.material.SetFloat("_ColorMaxValue", colorMaxValue);
    }
}