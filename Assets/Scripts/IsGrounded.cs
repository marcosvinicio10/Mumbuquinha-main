using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [Header("Configuração do chão")]
    public LayerMask groundLayer;               // Apenas a camada Ground
    public float groundCheckDistance = 0.3f;    // Distância do raycast

    public bool isGrounded { get; private set; }

    void Update()
    {
        // Origem do raycast: do centro do personagem para baixo
        Vector3 origin = transform.position;

        // Raycast para baixo
        isGrounded = Physics.Raycast(origin, Vector3.down, groundCheckDistance, groundLayer);

        // Desenha o raycast no editor para debug
        Debug.DrawRay(origin, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);



        if (isGrounded )
        {

            Debug.Log("Esta no chao");

        }




    }
}