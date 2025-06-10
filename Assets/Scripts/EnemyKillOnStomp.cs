using UnityEngine;

public class EnemyKillOnStomp : MonoBehaviour
{
    [Tooltip("Altura abaixo da qual o jogador deve estar para contar como 'pisando em cima'")]
    public float stompHeightOffset = 0.3f;

    void OnTriggerEnter(Collider other)
    {
        // Verifica se � o jogador
        if (other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;

            // Verifica se o jogador est� vindo de cima
            if (playerTransform.position.y > transform.position.y + stompHeightOffset)
            {
                // Destr�i o inimigo
                Destroy(gameObject);

                // D� um impulso no jogador para simular bounce
                MarioLikeMovement movement = other.GetComponent<MarioLikeMovement>();
                if (movement != null)
                {
                    movement.Bounce();
                }
            }
        }
    }
}
