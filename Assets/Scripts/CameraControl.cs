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
        // ������ �������� �� waterLevel + focusOffset�� ī�޶� ���� �߽��� ���̷� ��
        cameraFocus.y = uiControl.generatedWaterLevel + focusOffsetHeight;

        // ȸ���̵�
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

        // Mathf.Cos(), Mathf.Sin() ������� ����/���� ȸ���� 60�й����� ȣ�������� ��ȯ
        float horizontalRotationRad = Mathf.Deg2Rad * horizontalRotation;
        float verticalRotationRad = Mathf.Deg2Rad * verticalRotation;

        // ī�޶�� ������� ���̿� �̵� �ӵ� ����ϰ� �ϱ�
        movementSpeed = Mathf.Clamp(movementSpeedPerDistance * (distance * Mathf.Sin(verticalRotationRad) + focusOffsetHeight), minMovementSpeed, maxMovementSpeed);

        // �����̵�
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

        // ��-�� �� �ƿ�
        if (Input.GetKey(KeyCode.I))
        {
            distance -= zoomSpeed;
        }
        if (Input.GetKey(KeyCode.O))
        {
            distance += zoomSpeed;
        }

        // �Ÿ�, ȸ�� �ݰ� �ִ� �ּ� ����
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
        verticalRotation = Mathf.Clamp(verticalRotation, 0, 90);

        // ���� ī�޶� �̵��� ȸ��
        transform.position = cameraFocus + new Vector3(
            - distance * Mathf.Cos(verticalRotationRad) * Mathf.Sin(horizontalRotationRad),
            distance * Mathf.Sin(verticalRotationRad),
            - distance * Mathf.Cos(verticalRotationRad) * Mathf.Cos(horizontalRotationRad));
        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}