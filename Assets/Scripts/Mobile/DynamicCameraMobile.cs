using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DynamicCameraMobile : MonoBehaviour
{
    [Header("Referências")]
    public Transform cameraTransform;
    private Transform alvo;

    [Header("Configuração")]
    public float alturaFixa = 2f;
    public float distancia = 6f;
    public Vector2 limiteZoom = new Vector2(4f, 12f);
    public float suavidade = 10f;

    [Header("Controles")]
    public float sensibilidadeRotacao = 0.3f;
    public float sensibilidadeZoom = 0.01f;

    [Header("Tremor")]
    public float duracaoTremor = 0.5f;
    public float intensidadeTremor = 0.3f;

    [Header("Proteções de Input")]
    public bool bloquearJoystick = true;
    public RectTransform joystickArea; // área de proteção (ex: background do joystick)

    public bool bloquearLadoEsquerdo = false; // evita rotação no lado esquerdo da tela

    private float anguloY = 0f;
    private Vector2 toqueAnterior1, toqueAnterior2;
    private bool usandoZoom = false;
    private bool tremorAtivo = false;

    void Start()
    {
        StartCoroutine(EncontrarAlvo());
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        ProcessarInputToque();
        AtualizarCamera();
    }

    void ProcessarInputToque()
    {
        if (Input.touchCount == 1)
        {
            Touch toque = Input.GetTouch(0);

            // Se o toque estiver sobre UI (como o joystick), ignora
            if (EventSystem.current.IsPointerOverGameObject(toque.fingerId)) return;

            // Se estiver na área do joystick, ignora (apenas se ativado)
            if (bloquearJoystick && joystickArea != null)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(joystickArea, toque.position, null)) return;
            }

            // Se estiver na metade esquerda da tela, ignora (opcional)
            if (bloquearLadoEsquerdo && toque.position.x < Screen.width * 0.5f) return;

            // Gira a câmera horizontalmente
            if (toque.phase == TouchPhase.Moved)
            {
                anguloY += toque.deltaPosition.x * sensibilidadeRotacao;
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            // Se qualquer dedo estiver sobre a UI, ignora o zoom
            if (EventSystem.current.IsPointerOverGameObject(t1.fingerId) ||
                EventSystem.current.IsPointerOverGameObject(t2.fingerId)) return;

            if (!usandoZoom)
            {
                toqueAnterior1 = t1.position;
                toqueAnterior2 = t2.position;
                usandoZoom = true;
            }
            else
            {
                float distAnterior = Vector2.Distance(toqueAnterior1, toqueAnterior2);
                float distAtual = Vector2.Distance(t1.position, t2.position);
                float delta = distAnterior - distAtual;

                distancia += delta * sensibilidadeZoom;
                distancia = Mathf.Clamp(distancia, limiteZoom.x, limiteZoom.y);

                toqueAnterior1 = t1.position;
                toqueAnterior2 = t2.position;
            }
        }
        else
        {
            usandoZoom = false;
        }
    }

    void AtualizarCamera()
    {
        Quaternion rotacao = Quaternion.Euler(0f, anguloY, 0f);
        Vector3 offset = rotacao * new Vector3(0f, 0f, -distancia);

        Vector3 foco = alvo.position + Vector3.up * alturaFixa;
        Vector3 destino = foco + offset;

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, destino, Time.deltaTime * suavidade);
        cameraTransform.LookAt(foco);
    }

    public void IniciarTremor()
    {
        if (!tremorAtivo)
            StartCoroutine(CameraShake());
    }

    IEnumerator CameraShake()
    {
        tremorAtivo = true;
        Vector3 original = cameraTransform.localPosition;

        float t = 0f;
        while (t < duracaoTremor)
        {
            float x = Random.Range(-1f, 1f) * intensidadeTremor;
            float y = Random.Range(-1f, 1f) * intensidadeTremor;

            cameraTransform.localPosition = original + new Vector3(x, y, 0);
            t += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = original;
        tremorAtivo = false;
    }

    IEnumerator EncontrarAlvo()
    {
        int tentativas = 0;
        while (alvo == null && tentativas < 300)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                alvo = obj.transform;
                yield break;
            }

            tentativas++;
            yield return new WaitForSeconds(0.02f);
        }

        Debug.LogWarning("FixedFollowCameraOrbit: Player não encontrado!");
    }
}
