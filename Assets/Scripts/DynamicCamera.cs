using UnityEngine;
using System.Collections;

public class DynamicCamera : MonoBehaviour
{
    // ————— REFERÊNCIAS E CONFIGURAÇÕES —————
    [Header("Referências")]
    private Transform alvo;
    public Transform cameraTransform;

    [Header("Órbita")]
    public float altura = 2f;
    public float distanciaPadrao = 10f;
    public float distanciaMin = 7.5f;
    public float distanciaMax = 15f;
    public float sensibilidadeX = 4f;
    public float sensibilidadeY = 1.5f;
    public Vector2 limiteRotY = new Vector2(-40f, 80f);
    public float suavidadeZoom = 10f;

    [Header("Colisão")]
    public LayerMask camadasDeColisao;
    public float raioEsfera = 0.3f;

    [Header("Tremor")]
    public float duracaoTremor = 0.5f;
    public float intensidadeTremor = 0.3f;

    // ————— VARIÁVEIS INTERNAS —————
    private float anguloX = 0f;
    private float anguloY = 0f;
    private float distanciaAtual;
    private bool tremorAtivo = false;

    // ————— INICIALIZAÇÃO —————
    void Start()
    {
        StartCoroutine(EncontrarAlvoPorTag());

        Cursor.lockState = CursorLockMode.Locked;

        anguloX = transform.eulerAngles.y;
        anguloY = transform.eulerAngles.x;

        distanciaAtual = distanciaPadrao;
        cameraTransform.localPosition = Vector3.zero;
    }

    // ————— LOOP PRINCIPAL (LATEUPDATE) —————
    void LateUpdate()
    {
        // — Rotação da Câmera
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeX;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeY;

        anguloX += mouseX;
        anguloY -= mouseY;
        anguloY = Mathf.Clamp(anguloY, limiteRotY.x, limiteRotY.y);

        // — Zoom com Scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distanciaPadrao -= scroll * 5f;
        distanciaPadrao = Mathf.Clamp(distanciaPadrao, distanciaMin, distanciaMax);

        // — Cálculo de posição e rotação
        Quaternion rotacao = Quaternion.Euler(anguloY, anguloX, 0);
        Vector3 direcaoDesejada = rotacao * Vector3.back;

        if (alvo == null) return;
        Vector3 posAlvo = alvo.position + Vector3.up * altura;

        // — Verifica colisão com cenário
        RaycastHit hit;
        float distanciaFinal = distanciaPadrao;

        if (Physics.SphereCast(posAlvo, raioEsfera, direcaoDesejada, out hit, distanciaPadrao, camadasDeColisao))
        {
            distanciaFinal = hit.distance - 0.1f;
        }

        distanciaAtual = Mathf.Lerp(distanciaAtual, distanciaFinal, Time.deltaTime * suavidadeZoom);

        // — Aplica posição e rotação
        transform.position = posAlvo;
        transform.rotation = rotacao;

        if (!tremorAtivo)
        {
            cameraTransform.localPosition = new Vector3(0, 0, -distanciaAtual);
        }

        // — Ativador de tremor manual (tecla T)
        if (Input.GetKeyDown(KeyCode.T))
        {
            IniciarTremor();
        }
    }

    // ————— FUNÇÃO: Iniciar Tremor —————
    public void IniciarTremor()
    {
        if (!tremorAtivo)
        {
            StartCoroutine(CameraShake());
        }
    }

    // ————— CORROTINA: Tremor de Câmera —————
    IEnumerator CameraShake()
    {
        tremorAtivo = true;
        Vector3 posOriginal = cameraTransform.localPosition;

        float tempo = 0f;
        while (tempo < duracaoTremor)
        {
            float x = Random.Range(-1f, 1f) * intensidadeTremor;
            float y = Random.Range(-1f, 1f) * intensidadeTremor;

            cameraTransform.localPosition = posOriginal + new Vector3(x, y, 0);

            tempo += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = new Vector3(0, 0, -distanciaAtual);
        tremorAtivo = false;
    }
    IEnumerator EncontrarAlvoPorTag()
    {
        while (alvo == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                alvo = obj.transform;
            }
            yield return null; // espera 1 frame e tenta de novo
        }
    }
}
