using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class MarioLikeMovement : MonoBehaviour
{
    [Header("Referências")]
    public Transform cameraTransform;

    [Header("Movimentação")]
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public float acceleration = 20f;
    public float deceleration = 10f;
    public float airControl = 0.5f;
  


    [Header("Pulo")]
    public float jumpForce = 10f;
    public float gravity = -25f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.1f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 currentMoveDirection;

    private float lastGroundedTime;
    private float lastJumpPressedTime = -100f;

    [Header("Anim")]
    public bool IsWalking {  get; private set; }
    public Animator animtr;
    


    [Header("Interações")]
    public float trampolimForce = 32f;


    void Start()
    {
       
        controller = GetComponent<CharacterController>();


        

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Trampolim"))
        {
            animtr.SetTrigger("IsJumping");
            velocity.y = trampolimForce;
        }
    }





    void Update()
    {
       
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0f, v).normalized;
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 targetDir = camForward * inputDir.z + camRight * inputDir.x;
        bool isGrounded = controller.isGrounded;

        //Esse Código é para o Coyote Time
        if (isGrounded)
        {
            lastGroundedTime = Time.time;
            if (velocity.y < 0)
                velocity.y = -2f;
        }
        // Pulo (jump buffer)
        if (Input.GetButtonDown("Jump")) 
            lastJumpPressedTime = Time.time;
            


        // Direção baseada na câmera
    
        float controlFactor = isGrounded ? 1f : airControl;
        if (targetDir.magnitude > 0.1f)
        {
            currentMoveDirection = Vector3.MoveTowards(currentMoveDirection, targetDir, acceleration * controlFactor * Time.deltaTime);
            Quaternion targetRot = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
        else
        {
            currentMoveDirection = Vector3.MoveTowards(currentMoveDirection, Vector3.zero, deceleration * Time.deltaTime);
        }

        // Aplica movimento horizontal
        Vector3 move = currentMoveDirection * moveSpeed;

        // Pulo com coyote time + jump buffer
        if (Time.time - lastGroundedTime <= coyoteTime && Time.time - lastJumpPressedTime <= jumpBufferTime)
        {
            velocity.y = jumpForce;
            lastJumpPressedTime = -100f; // reseta
        }

        // Sustentação do pulo: se segurar o botão, mantém no ar mais tempo
        if (!isGrounded && Input.GetButton("Jump") && velocity.y > 0)
        {
            velocity.y += gravity * Time.deltaTime; // gravidade reduzida
        }
        else
        {
            velocity.y += gravity * Time.deltaTime; // gravidade normal
          
        }

        // Aplica movimentação
        controller.Move((move + new Vector3(0, velocity.y, 0)) * Time.deltaTime);


        IsWalking = new Vector3(move.x, 0f, move.z).magnitude > 0.1f;

        if (Input.GetButtonDown("Jump"))
        {
            animtr.SetTrigger("IsJumping");

        }


        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 16f;

            Debug.Log("Key Pressed");

        }

        else
        {
            moveSpeed = 10f;


        }




        // Colisões
   












}






    public void Bounce()
    {
        velocity.y = jumpForce * 0.8f; // Pode ajustar o multiplicador
    }







}