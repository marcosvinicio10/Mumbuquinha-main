using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement3D : MonoBehaviour
{
    // ————— REFERÊNCIAS E CONFIGURAÇÕES —————

    private Transform referenciaCamera;

    [Header("Movimentação")]
    public float velocidade = 6f;
    public float suavidadeRotacao = 10f;

    [Header("Pulo")]
    public float forcaPuloMinima = 10f;
    public float forcaPuloMaxima = 16f;
    public float tempoMaximoCarregamento = 0.5f;
    public float gravidade = -20f;
    public float trampolimForce = 30f;

    [Header("Dash")]
    public float forcaDash = 12f;
    public float tempoCooldownDash = 2f;

    private CharacterController controller;
    private Vector3 direcaoVelocidade;
    private float velocidadeVertical;
    private float tempoCarregandoPulo;
    private bool carregandoPulo;
    private bool podeDash = true;
    private bool estaNoChao => controller.isGrounded;


    private Animator animator;

    [Header("Pulo Avançado")]
    public float tempoCoyote = 0.2f; // tempo de coyote em segundos

    private float tempoDesdeUltimoChao = 0f;





    void Start()
    {
        GameObject obj = GameObject.Find("Main Camera");
        if (obj != null)
            referenciaCamera = obj.transform;
        controller = GetComponent<CharacterController>();

        // Achar automaticamente o Animator no personagem 3D filho
        animator = GetComponentInChildren<Animator>();


    }

    void Update()
    {
        ReceberEntradaMovimento();
        AplicarGravidade();

        if (estaNoChao)
            tempoDesdeUltimoChao = 0f;
        else
            tempoDesdeUltimoChao += Time.deltaTime;




        if (Input.GetKeyDown(KeyCode.Space) && tempoDesdeUltimoChao <= tempoCoyote)
        {
            velocidadeVertical = forcaPuloMinima;
            tempoCarregandoPulo = 0f;
            carregandoPulo = true;

            // Dispara a animação de pulo
            if (animator != null)
                animator.SetTrigger("IsJumping");
        }

        // Segura espaço = aumenta altura do pulo
        if (Input.GetKey(KeyCode.Space) && carregandoPulo)
        {
            tempoCarregandoPulo += Time.deltaTime;
            if (tempoCarregandoPulo < tempoMaximoCarregamento)
            {
                float incremento = (forcaPuloMaxima - forcaPuloMinima) * (Time.deltaTime / tempoMaximoCarregamento);
                velocidadeVertical += incremento;
            }
            else
            {
                carregandoPulo = false;
            }
        }

        // Solta espaço = para de aumentar altura
        if (Input.GetKeyUp(KeyCode.Space))
        {
            carregandoPulo = false;
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
            RealizarDash();

        // Movimento
        controller.Move((direcaoVelocidade + Vector3.up * velocidadeVertical) * Time.deltaTime);

        AtualizarAnimacoes();

    }

    void AtualizarAnimacoes()
    {
        if (animator == null) return;

        bool andando = direcaoVelocidade.magnitude > 0.1f;
        animator.SetBool("IsWalking", andando);
    }



    // ————— FUNÇÕES DE MOVIMENTO —————

    public void ReceberEntradaMovimento()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direcaoEntrada = new Vector3(horizontal, 0f, vertical).normalized;

        if (direcaoEntrada.magnitude >= 0.1f)
        {
            float anguloAlvo = Mathf.Atan2(direcaoEntrada.x, direcaoEntrada.z) * Mathf.Rad2Deg + referenciaCamera.eulerAngles.y;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloAlvo, ref suavidadeRotacao, 0.1f);

            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            Vector3 direcaoMovimento = Quaternion.Euler(0f, anguloAlvo, 0f) * Vector3.forward;
            direcaoVelocidade = direcaoMovimento.normalized * velocidade;
        }
        else
        {
            direcaoVelocidade = Vector3.zero;
        }
    }

    // ————— GRAVIDADE —————

    void AplicarGravidade()
    {
        if (estaNoChao && velocidadeVertical < 0)
            velocidadeVertical = -2f;
        else
            velocidadeVertical += gravidade * Time.deltaTime;
    }

    // ————— DASH —————

    void RealizarDash()
    {
        if (!podeDash || direcaoVelocidade == Vector3.zero) return;

        Vector3 direcaoDash = direcaoVelocidade.normalized;
        StartCoroutine(FazerDash(direcaoDash));
    }

    IEnumerator FazerDash(Vector3 direcao)
    {
        podeDash = false;
        float tempo = 0.2f;
        float t = 0f;

        while (t < tempo)
        {
            controller.Move(direcao * forcaDash * Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(tempoCooldownDash);
        podeDash = true;
    }

    // ————— TRAMPOLIM —————

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Trampolim"))
        {
            Debug.Log("Trampolim ativado!");
            velocidadeVertical = trampolimForce;
        }
    }
}
