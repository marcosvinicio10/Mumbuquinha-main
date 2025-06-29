using UnityEngine;

public class CameraFollowOrbit : MonoBehaviour
{
    [Header("Refer�ncia")]
    public Transform target;

    [Header("Configura��o da C�mera")]
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 3.0f;

    [Header("Limites de rota��o vertical")]
    public float minY = -20f;
    public float maxY = 60f;

    [Header("Camadas que a c�mera deve colidir")]
    public LayerMask collisionLayers;

    private float currentX = 0f;
    private float currentY = 20f;

    private bool cursorTravado = false;

    void Start()
    {
        // Come�a com o cursor livre e vis�vel
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // ESC libera o cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorTravado = false;
        }

        // Primeiro clique trava o cursor, se ainda n�o estiver travado
        if (!cursorTravado && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorTravado = true;
        }

        if (cursorTravado)
        {
            // Entrada do mouse
            currentX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;
            currentY = Mathf.Clamp(currentY, minY, maxY);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredDir = rotation * new Vector3(0, 0, -distance);
        Vector3 targetCenter = target.position + Vector3.up * height;
        Vector3 desiredPosition = targetCenter + desiredDir;


        if (Physics.Raycast(targetCenter, desiredDir.normalized, out RaycastHit hit, distance, collisionLayers))
        {
            Vector3 novaPosicao = hit.point - desiredDir.normalized * 0.1f;
            transform.position = novaPosicao;
        }
        else
        {
            transform.position = desiredPosition;
        }

        // Evita que a c�mera v� para baixo do jogador (opcional)
        if (transform.position.y < target.position.y - 0.5f)
        {
            Vector3 pos = transform.position;
            pos.y = target.position.y - 0.5f;
            transform.position = pos;
        }

        transform.LookAt(targetCenter);
    }
}