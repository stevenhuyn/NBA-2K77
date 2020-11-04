using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public float amplitude = 0.05f;
    public float speed = 9.3f;
    public float waveLength = 27;
    public int radiusVertices = 5;
    public Material material;

    private Light pointLight;

    void Start() {
        // TODO: fix this, shouldn't work as we're using a Directional Light
        pointLight = GameObject.Find("Directional Light").GetComponent<Light>();

        var meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = CreateMesh();
        
        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        meshRenderer.material.shader = Shader.Find("Unlit/WaveShader");
        meshRenderer.material.SetColor("_PointLightColor", pointLight.color);
        meshRenderer.material.SetVector("_PointLightPosition", pointLight.transform.position);
        meshRenderer.material.SetFloat("_Amplitude", amplitude);
        meshRenderer.material.SetFloat("_Speed", speed);
        meshRenderer.material.SetFloat("_Wavelength", waveLength);
    }

    /*private Mesh CreateMesh() {
        // Create a circular mesh
        Mesh m = new Mesh();
        m.name = "WaveCircle";
        var vertices = new List<Vector3>();
        var colors = new List<Color>();
        var triangles = new List<int>();
        Color currentColor = color;
        currentColor.a = 0.7f;

        // Add origin
        Vector3 origin = Vector3.zero;
        vertices.Add(origin);
        colors.Add(currentColor);

        for (int i = 0; i < radiusVertices; i++) {
            float angle = i / radiusVertices * 2 * Mathf.PI;
            Vector3 v = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            vertices.Add(v);

            float c = currentColor.r + Random.Range(-0.1f, 0.1f);
            currentColor = new Color(c, c, c);
            colors.Add(currentColor);
        }

        /*for (int i = 1; i < radiusVertices; i++) {
            triangles.Add(i);
            triangles.Add(i + 1);
            triangles.Add(0);
        }*/

        /*triangles.Add(radiusVertices);
        triangles.Add(1);
        triangles.Add(0);

        m.vertices = vertices.ToArray();
        m.colors = colors.ToArray();
        m.triangles = triangles.ToArray();

        return m;
    }*/

    Mesh CreateMesh() {
        // Adapted from Workshop 2 Cone Mesh work

        Mesh m = new Mesh();
        m.name = "WaveCircle";

        // Define the vertices. These are the "points" in 3D space that allow us to
        // construct 3D geometry (by connecting groups of 3 points into triangles).
        var vertices = new Vector3[radiusVertices + 1];
        vertices[0] = Vector3.zero; // centre
        float angle = 0;
        float radius = 1;
        for (int i = 1; i <= radiusVertices; i++) {
            vertices[i] = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            angle += Mathf.PI * 2 / radiusVertices;
        }

        // Define the vertex colours
        var colors = new Color[radiusVertices + 1];
        Color currentColor = material.color;
        colors[0] = currentColor;
        for (int i = 1; i <= radiusVertices; i++) {
            float c = currentColor.r + Random.Range(-0.1f, 0.1f);
            currentColor = new Color(c, c, c);
            colors[i] = currentColor;
        }

        // Automatically define the triangles based on the number of vertices
        var triangles = new int[radiusVertices * 3];
        int j;
        for (j = 0; j < radiusVertices - 1; j += 1) {
            triangles[3*j] = 0;
            triangles[3*j+1] = j + 1;
            triangles[3*j+2] = j + 2;
        }
        triangles[3*j] = 0;
        triangles[3*j+1] = j + 1;
        triangles[3*j+2] = 1;

        m.vertices = vertices;
        m.colors = colors;
        m.triangles = triangles;

        return m;
    }
}