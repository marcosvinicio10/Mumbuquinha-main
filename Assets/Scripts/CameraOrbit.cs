using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float defaultDistance = 5f;
    public float height = 2f;
    public float rotationSpeed = 100f;
    public Joystick lookJoystick;

    public LayerMask groundLayer;         // Layer para detectar colisões (ex: Ground)

    private float currentAngle = 0f;
    private float currentDistance;

    void Start()
    {
        currentDistance = defaultDistance;
    }

    void LateUpdate()
    {
        // Input do joystick (X para girar horizontalmente)
        float horizontalInput = lookJoystick.Horizontal();
        currentAngle += horizontalInput * rotationSpeed * Time.deltaTime;

        // Calcula a rotação e direção da câmera
        Quaternion rotation = Quaternion.Euler(0, currentAngle, 0);
        Vector3 desiredPosition = target.position + rotation * new Vector3(0, height, -defaultDistance);

        // Raycast para detectar obstáculos
        Vector3 rayDirection = (desiredPosition - target.position).normalized;
        float rayDistance = defaultDistance;

        RaycastHit hit;
        if (Physics.Raycast(target.position + Vector3.up * height * 0.5f, rayDirection, out hit, defaultDistance, groundLayer))
        {
            currentDistance = hit.distance - 0.1f; // Aproxima um pouco da parede
        }
        else
        {
            currentDistance = defaultDistance;
        }

        // Recalcula a posição real com a distância ajustada
        Vector3 finalPosition = target.position + rotation * new Vector3(0, height, -currentDistance);

        transform.position = finalPosition;
        transform.LookAt(target.position + Vector3.up * height * 0.5f);
    }
}
