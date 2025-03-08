using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    public int xSize = 50; // x 축 방향으로 Mesh의 사각형 개수
    public int zSize = 50; // z 축 방향으로 Mesh의 사각형 개수
    public float waterLevel = 0f; // 지형의 최저 높이
    public float perlinDensity1 = .1f;
    public float perlinDensity2 = 1f;
    public float perlinMagnitude1 = 2f;
    public float perlinMagnitude2 = 2f;
    public float perlinPower1 = 4;
    public float perlinPower2 = 2;

    void Start()
    {

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateTerrain();
        
    }

    public void GenerateTerrain()
    {
            CreateShape();
            UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++, i++)
            {
                float y = Mathf.Max( // perlinNoise로 y값 만들기
                    waterLevel,
                    Mathf.Pow(
                        Mathf.Clamp01(Mathf.PerlinNoise(x * perlinDensity1, z * perlinDensity1)) * perlinMagnitude1,
                        perlinPower1)
                    - Mathf.Pow(
                        Mathf.Clamp01(Mathf.PerlinNoise((z + 20f) * perlinDensity2, (x + 20f) * perlinDensity2)) * perlinMagnitude2,
                    perlinPower2));

                vertices[i] = new Vector3(x, y, z); // vertex 만들기
            }
        }

        triangles = new int[xSize * zSize * 3 * 2];

        for (int i = 0, z = 0; z < zSize; z++) 
        {
            for (int x = 0; x < xSize; x++, i+=6)
            {
                // Mesh 사각형(삼각형 두 개) 만들기
                triangles[i] = (xSize + 1) * z + x;
                triangles[i + 1] = (xSize + 1) * (z + 1) + x;
                triangles[i + 2] = (xSize + 1) * z + x + 1;
                triangles[i + 3] = (xSize + 1) * z + x + 1;
                triangles[i + 4] = (xSize + 1) * (z + 1) + x;
                triangles[i + 5] = (xSize + 1) * (z + 1) + x + 1;
            }
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos() // Scene에서 점으로 vertex 표시
    {

        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}
