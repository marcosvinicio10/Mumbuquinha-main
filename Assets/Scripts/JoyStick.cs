using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBG; // Fundo do joystick(a parte fixa).
    public RectTransform joystickHandle; // Alavanca do joystick(parte que se move).
    public Vector2 inputDirection; // Vetor de direção baseado na posição do toque.

    private float joystickRadius; // Guarda o raio do joystick, usado pra limitar o movimento da alavanca.


    private void Start()
    {
        joystickRadius = joystickBG.sizeDelta.x / 2;
    }

    public void OnPointerDown(PointerEventData eventData) // Quando o dedo toca o joystick, ele já chama OnDrag() pra atualizar a posição.
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG, eventData.position, eventData.pressEventCamera, out pos)) // Converte a posição do toque (eventData.position) pra um ponto local dentro do retângulo do joystickBG.
        {
            inputDirection = pos / joystickRadius;
            inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

            joystickHandle.anchoredPosition = inputDirection * joystickRadius * 0.5f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return inputDirection.x;
    }

    public float Vertical()
    {
        return inputDirection.y;
    }

    public Vector2 Direction()
    {
        return inputDirection;
    }

}
