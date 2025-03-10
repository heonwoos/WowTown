using System.CodeDom.Compiler;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    public MeshGenerator meshGenerator;

    public Slider xSize;
    public Slider zSize;
    public Slider waterLevel;
    public Slider perlinDensity1;
    public Slider perlinMagnitude1;
    public Slider perlinPower1;
    public Slider perlinDensity2;
    public Slider perlinMagnitude2;
    public Slider perlinPower2;

    public float generatedWaterLevel = 0f;

    void Start()
    {
        meshGenerator.GetComponent<MeshGenerator>();
    }
    private void Update()
    {
        // slider 값을 MeshGenerator의 변수에 동기화
        meshGenerator.xSize = (int)xSize.value;
        meshGenerator.zSize = (int)zSize.value;
        meshGenerator.waterLevel = waterLevel.value;
        meshGenerator.perlinDensity1 = perlinDensity1.value;
        meshGenerator.perlinMagnitude1 = perlinMagnitude1.value;
        meshGenerator.perlinPower1 = perlinPower1.value;
        meshGenerator.perlinDensity2 = perlinDensity2.value;
        meshGenerator.perlinMagnitude2 = perlinMagnitude2.value;
        meshGenerator.perlinPower2 = perlinPower2.value;

        if (Input.GetKeyDown(KeyCode.G)) // 'G' 키를 누르면 지형 생성
        {
            meshGenerator.GenerateTerrain();
            generatedWaterLevel = meshGenerator.waterLevel;
        }
    }
}
    