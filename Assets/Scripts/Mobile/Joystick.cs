using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // ————— CONFIGURAÇÕES DO JOYSTICK —————
    public RectTransform joystickBG;
    public RectTransform joystickHandle;
    [Range(0.1f, 1f)] public float handleRange = 1f;
    public Vector2 inputDirection;

    private float joystickRadius;
    private PlayerMovement3DMobile player;

    void Start()
    {
        joystickRadius = joystickBG.sizeDelta.x / 2f;
        StartCoroutine(EsperarPlayer());
    }

    IEnumerator EsperarPlayer()
    {
        while (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
                player = obj.GetComponent<PlayerMovement3DMobile>();

            yield return null;
        }
    }

    void Update()
    {
        if (player != null)
        {
            player.AtualizarDirecao(inputDirection);
        }
    }

    // ————— EVENTO: TOQUE INICIAL —————
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    // ————— EVENTO: ARRASTAR O DEDO —————
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG, eventData.position, eventData.pressEventCamera, out pos))
        {
            inputDirection = pos / joystickRadius;
            inputDirection = Vector2.ClampMagnitude(inputDirection, 1f);

            // Atualiza o handle respeitando o limite do círculo
            joystickHandle.localPosition = inputDirection * joystickRadius * handleRange;
        }
    }

    // ————— EVENTO: SOLTOU O DEDO —————
    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.localPosition = Vector3.zero;
    }

    // ————— UTILIDADES OPCIONAIS —————
    public float Horizontal() => inputDirection.x;
    public float Vertical() => inputDirection.y;
    public Vector2 Direction() => inputDirection;
}
