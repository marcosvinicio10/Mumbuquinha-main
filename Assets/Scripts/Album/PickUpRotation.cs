using UnityEngine;

public class PickUpRotation : MonoBehaviour
{
    public float velocidadeRot = 75f;

    void Update()
    {
        transform.Rotate(0, velocidadeRot * Time.deltaTime, 0);
    }
}
