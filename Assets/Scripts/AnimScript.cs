using UnityEngine;

public class AnimScript : MonoBehaviour
{
    private Animator animator;
    public MarioLikeMovement movementScript; // Refer�ncia para o script de movimenta��o

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (movementScript == null) return;

        animator.SetBool("IsWalking", movementScript.IsWalking);
    }
}