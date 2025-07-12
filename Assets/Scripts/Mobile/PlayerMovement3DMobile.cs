using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement3DMobile : MonoBehaviour
{
    // ————— MOVIMENTO —————
    [Header("Movimento")]
    [Tooltip("Velocidade de movimento do player")]
    public float velocidade = 9.5f;

    [Tooltip("Tempo para suavizar rotação")]
    public float suavidadeRotacao = 0.1f;

    // ————— PULO —————
    [Header("Pulo")]
    [Tooltip("Força do pulo ao tocar no botão")]
    public float forcaPulo = 10f;

    [Tooltip("Gravidade aplicada ao player")]
    public float gravidade = -20f;

    // ————— DASH —————
    [Header("Dash")]
    [Tooltip("Força aplicada ao dash")]
    public float forcaDash = 20f;

    [Tooltip("Tempo de recarga entre dashes")]
    public float tempoCooldownDash = 1.5f;

    // ————— INTERNOS —————
    private CharacterController controller;
    private Transform referenciaCamera;
    private Vector3 direcaoMovimento;
    private float velocidadeVertical;
    private bool podeDash = true;

    private float velocidadeRotacaoAtual;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        referenciaCamera = Camera.main?.transform;
    }

    void Update()
    {
        AplicarGravidade();

        Vector3 movimento = direcaoMovimento + Vector3.up * velocidadeVertical;
        controller.Move(movimento * Time.deltaTime);
    }

    public void AtualizarDirecao(Vector2 input)
    {
        if (referenciaCamera == null) return;

        Vector3 entrada = new Vector3(input.x, 0f, input.y).normalized;

        if (entrada.magnitude >= 0.1f)
        {
            float anguloAlvo = Mathf.Atan2(entrada.x, entrada.z) * Mathf.Rad2Deg + referenciaCamera.eulerAngles.y;

            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloAlvo, ref velocidadeRotacaoAtual, suavidadeRotacao);
            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            Vector3 direcao = Quaternion.Euler(0f, anguloAlvo, 0f) * Vector3.forward;
            direcaoMovimento = direcao.normalized * velocidade;
        }
        else
        {
            direcaoMovimento = Vector3.zero;
        }
    }

    public void Pular()
    {
        if (controller.isGrounded)
        {
            velocidadeVertical = forcaPulo;
        }
    }

    public void RealizarDash()
    {
        if (!podeDash || direcaoMovimento == Vector3.zero) return;

        StartCoroutine(FazerDash(direcaoMovimento.normalized));
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

    void AplicarGravidade()
    {
        if (controller.isGrounded && velocidadeVertical < 0)
        {
            velocidadeVertical = -2f;
        }
        else
        {
            velocidadeVertical += gravidade * Time.deltaTime;
        }
    }
}
