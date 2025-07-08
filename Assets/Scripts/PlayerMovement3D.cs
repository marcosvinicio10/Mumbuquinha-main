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
    public float tempoMaximoCarregamento = 1f;
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

    void Start()
    {
        GameObject obj = GameObject.Find("Main Camera");
        if (obj != null)
            referenciaCamera = obj.transform;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ReceberEntradaMovimento();
        AplicarGravidade();

        // Entrada de pulo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tempoCarregandoPulo = 0f;
            carregandoPulo = true;
        }

        if (Input.GetKey(KeyCode.Space) && carregandoPulo)
        {
            tempoCarregandoPulo += Time.deltaTime;
            tempoCarregandoPulo = Mathf.Clamp(tempoCarregandoPulo, 0f, tempoMaximoCarregamento);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            RealizarPulo();
            carregandoPulo = false;
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
            RealizarDash();

        // Movimento
        controller.Move((direcaoVelocidade + Vector3.up * velocidadeVertical) * Time.deltaTime);
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

    // ————— PULO —————

 

    

    IEnumerator CarregarPulo()
    {
        while (carregandoPulo && tempoCarregandoPulo < tempoMaximoCarregamento)
        {
            tempoCarregandoPulo += Time.deltaTime;
            yield return null;
        }
    }

    void RealizarPulo()
    {
        if (estaNoChao)
        {
            carregandoPulo = false;
            float t = Mathf.Clamp01(tempoCarregandoPulo / tempoMaximoCarregamento);
            float forca = Mathf.Lerp(forcaPuloMinima, forcaPuloMaxima, t);
            velocidadeVertical = forca;
        }
    }

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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Trampolim"))
        {
            Debug.Log("Trampolim ativado!");
            velocidadeVertical = trampolimForce;
        }
    }




}