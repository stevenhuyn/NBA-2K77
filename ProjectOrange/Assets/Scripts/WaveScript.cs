using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour {
    public float amplitude = 0.05f;
    public float speed = 9.3f;
    public float waveLength = 27;
    public float radius = 3;
    public int radiusVertices = 5;
    public int rings = 2;

    private Light pointLight;

    void Start() {
        // TODO: fix this, shouldn't work as we're using a Directional Light
        pointLight = GameObject.Find("Directional Light").GetComponent<Light>();

        //var meshFilter = gameObject.AddComponent<MeshFilter>();
        //meshFilter.mesh = CreateMesh();
        
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.shader = Shader.Find("Unlit/WaveShader");
        meshRenderer.material.SetColor("_PointLightColor", pointLight.color);
        meshRenderer.material.SetVector("_PointLightPosition", pointLight.transform.position);
        meshRenderer.material.SetFloat("_Amplitude", amplitude);
        meshRenderer.material.SetFloat("_Speed", speed);
        meshRenderer.material.SetFloat("_Wavelength", waveLength);
    }

    Mesh CreateMesh() {
        // Adapted from Workshop 2 Cone Mesh work

        Mesh m = new Mesh();
        m.name = "WaveCircle";

        // Define the vertices. These are the "points" in 3D space that allow us to
        // construct 3D geometry (by connecting groups of 3 points into triangles).
        var vertices = new Vector3[radiusVertices * rings + 1];
        vertices[0] = Vector3.zero; // centre
        for (int r = 1; r <= rings; r++) {
            float angle = -((r - 1) % rings) * 2 * Mathf.PI / radiusVertices;
            for (int i = (r - 1) * radiusVertices + 1; i <= r * radiusVertices; i++) {
                float dist = radius * r / rings;
                vertices[i] = new Vector3(dist * Mathf.Cos(angle), 0, dist * Mathf.Sin(angle));
                angle += Mathf.PI * 2 / radiusVertices;
            }
        }

        // Define the vertex colours
        //var colors = generateColors(vertices);

        // Automatically define the triangles based on the number of vertices
        var triangles = new int[radiusVertices * rings * 3];
        for (int r = 1; r <= rings; r++) {
            int j;
            for (j = (r - 1) * radiusVertices; j < r * radiusVertices - 1; j += 1) {
                triangles[3*j] = Mathf.Max(0, j - radiusVertices + 1);
                triangles[3*j+1] = j + 1;
                triangles[3*j+2] = j + 2;
            }
            triangles[3*j] = Mathf.Max(0, j - radiusVertices + 1);
            triangles[3*j+1] = j + 1;
            triangles[3*j+2] = (r - 1) * radiusVertices + 1;
        }

        m.vertices = vertices;
        //m.colors = colors;
        m.triangles = triangles;

        return m;
    }

    /*void Update() {
        var mesh = GetComponent<MeshFilter>().mesh;
        mesh.colors = generateColors(mesh.vertices);
    }*/

    Color[] generateColors(Vector3[] vertices) {
        var colors = new Color[radiusVertices * rings + 1];
        colors[0] = Random.ColorHSV(0, 1, 0, 0, 0.2f, 0.8f);
        for (int r = 1; r <= rings; r++) {
            for (int i = 1; i <= radiusVertices; i++) {
                colors[i] = Random.ColorHSV(0, 1, 0, 0, 0.2f, 0.8f);
                colors[i].a = 0.7f;
            }
        }
        return colors;
    }

    // from https://stackoverflow.com/questions/53406534/procedural-circle-mesh-with-uniform-faces
    // TODO: integrate
    /*public int resolution = 4;

    // Use this for initialization
    void Start() {
        GetComponent<MeshFilter>().mesh = GenerateCircle(resolution);
    }

    // Get the index of point number 'x' in circle number 'c'
    static int GetPointIndex(int c, int x) {
        if (c < 0) return 0; // In case of center point
        x = x % ((c + 1) * 6); // Make the point index circular
                               // Explanation: index = number of points in previous circles + central point + x
                               // hence: (0+1+2+...+c)*6+x+1 = ((c/2)*(c+1))*6+x+1 = 3*c*(c+1)+x+1

        return (3 * c * (c + 1) + x + 1);
    }

    public static Mesh GenerateCircle(int res) {

        float d = 1f / res;

        var vtc = new List<Vector3>();
        vtc.Add(Vector3.zero); // Start with only center point
        var tris = new List<int>();

        // First pass => build vertices
        for (int circ = 0; circ < res; ++circ) {
            float angleStep = (Mathf.PI * 2f) / ((circ + 1) * 6);
            for (int point = 0; point < (circ + 1) * 6; ++point) {
                vtc.Add(new Vector2(
                    Mathf.Cos(angleStep * point),
                    Mathf.Sin(angleStep * point)) * d * (circ + 1));
            }
        }

        // Second pass => connect vertices into triangles
        for (int circ = 0; circ < res; ++circ) {
            for (int point = 0, other = 0; point < (circ + 1) * 6; ++point) {
                if (point % (circ + 1) != 0) {
                    // Create 2 triangles
                    tris.Add(GetPointIndex(circ - 1, other + 1));
                    tris.Add(GetPointIndex(circ - 1, other));
                    tris.Add(GetPointIndex(circ, point));
                    tris.Add(GetPointIndex(circ, point));
                    tris.Add(GetPointIndex(circ, point + 1));
                    tris.Add(GetPointIndex(circ - 1, other + 1));
                    ++other;
                } else {
                    // Create 1 inverse triange
                    tris.Add(GetPointIndex(circ, point));
                    tris.Add(GetPointIndex(circ, point + 1));
                    tris.Add(GetPointIndex(circ - 1, other));
                    // Do not move to the next point in the smaller circle
                }
            }
        }

        // Create the mesh
        var m = new Mesh();
        m.SetVertices(vtc);
        m.SetTriangles(tris, 0);
        m.RecalculateNormals();
        m.UploadMeshData(true);

        return m;

    }*/
}