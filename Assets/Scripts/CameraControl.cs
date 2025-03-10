using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public UIControl uiControl;

    public float horizontalRotationSpeed = 1.5f;
    public float verticalRotationSpeed = 1.5f;
    public float movementSpeedPerDistance = 0.05f;
    public float zoomSpeed = 0.2f;
    public float minMovementSpeed = 0.2f;
    public float maxMovementSpeed = 2f;
    public float minZoom = 1f;
    public float maxZoom = 50f;
    public float focusOffsetHeight = 1f;
    float horizontalRotation = 45f;
    float verticalRotation = 25f;
    float distance = 23.66f;
    float movementSpeed;
    Vector3 cameraFocus = new Vector3(12.07f, 0f, 12.07f);

    private void Start()
    {
        uiControl.GetComponent<UIControl>();
    }

    void FixedUpdate()
    {
        // 지형을 생성했을 때 waterLevel + focusOffset을 카메라 초점 중심의 높이로 함
        cameraFocus.y = uiControl.generatedWaterLevel + focusOffsetHeight;

        // 회전이동
        if (Input.GetKey(KeyCode.Q))
        {
            horizontalRotation += horizontalRotationSpeed;
        }
        if (Input.GetKey(KeyCode.E)) {
            horizontalRotation -= horizontalRotationSpeed;
        }
        if (Input.GetKey(KeyCode.R))
        {
            verticalRotation += verticalRotationSpeed;
        }
        if (Input.GetKey(KeyCode.F))
        {
            verticalRotation -= verticalRotationSpeed;
        }

        // Mathf.Cos(), Mathf.Sin() 사용위해 수평/수직 회전을 60분법에서 호도법으로 변환
        float horizontalRotationRad = Mathf.Deg2Rad * horizontalRotation;
        float verticalRotationRad = Mathf.Deg2Rad * verticalRotation;

        // 카메라와 지면사이 높이에 이동 속도 비례하게 하기
        movementSpeed = Mathf.Clamp(movementSpeedPerDistance * (distance * Mathf.Sin(verticalRotationRad) + focusOffsetHeight), minMovementSpeed, maxMovementSpeed);

        // 평행이동
        if (Input.GetKey(KeyCode.W))
        {
            cameraFocus += new Vector3(Mathf.Sin(horizontalRotationRad), 0, Mathf.Cos(horizontalRotationRad)) * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraFocus += new Vector3(-Mathf.Cos(horizontalRotationRad), 0, Mathf.Sin(horizontalRotationRad)) * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraFocus += new Vector3(-Mathf.Sin(horizontalRotationRad), 0, -Mathf.Cos(horizontalRotationRad)) * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraFocus += new Vector3(Mathf.Cos(horizontalRotationRad), 0, -Mathf.Sin(horizontalRotationRad)) * movementSpeed;
        }

        // 줌-인 앤 아웃
        if (Input.GetKey(KeyCode.I))
        {
            distance -= zoomSpeed;
        }
        if (Input.GetKey(KeyCode.O))
        {
            distance += zoomSpeed;
        }

        // 거리, 회전 반경 최대 최소 설정
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
        verticalRotation = Mathf.Clamp(verticalRotation, 0, 90);

        // 실제 카메라 이동과 회전
        transform.position = cameraFocus + new Vector3(
            - distance * Mathf.Cos(verticalRotationRad) * Mathf.Sin(horizontalRotationRad),
            distance * Mathf.Sin(verticalRotationRad),
            - distance * Mathf.Cos(verticalRotationRad) * Mathf.Cos(horizontalRotationRad));
        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}