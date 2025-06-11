using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovAuto : MonoBehaviour
{
    [Header("Configurações")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public float gravity = -20f;

    [Header("Detecção de chão")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Verifica se está no chão com Raycast 
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Mantém o personm no chão
        }

        // Movimento automático no eixo X
        Vector3 move = Vector3.right;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Pulo
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpForce;
        }

        // Aplica gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}