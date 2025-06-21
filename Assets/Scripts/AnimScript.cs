using UnityEngine;

public class AnimScript : MonoBehaviour
{
    private Animator animator;
    public MarioLikeMovement movementScript; // Referência para o script de movimentação

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