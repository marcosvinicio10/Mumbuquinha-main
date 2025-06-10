using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Configurações")]
    public Transform player;  // Referência para o jogador
    public float smoothSpeed = 0.125f;  // Velocidade de suavização do movimento
    public Vector3 offset;  // Distância fixa da câmera em relação ao jogador

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            // A câmera sempre segue o jogador com a posição ajustada pelo 'offset'
            Vector3 desiredPosition = player.position + offset;

            // Suaviza a transição da posição da câmera para a posição desejada
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

            // Atualiza a posição da câmera
            transform.position = smoothedPosition;

            // A câmera deve manter a rotação fixa (não a segue)
            // Mantém o ângulo fixo da câmera sem alterar
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}