using UnityEngine;

public class ShadowProjector : MonoBehaviour
{
    public Transform target;         // O personagem
    public float offsetY = 0.05f;    // Pequena elevação para evitar flickering
    public float maxDistance = 20f;  // Máxima distância do raycast
    public GameObject shadow;        // A imagem da sombra (ex: um quad com textura)

    void Update()
    {
        if (Physics.Raycast(target.position, Vector3.down, out RaycastHit hit, maxDistance))
        {
            shadow.transform.position = hit.point + Vector3.up * offsetY;

            // Alinha a sombra à superfície
            shadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            if (!shadow.activeSelf)
                shadow.SetActive(true);
        }
        else
        {
            // Se não atingir nada, desativa a sombra
            if (shadow.activeSelf)
                shadow.SetActive(false);
        }
    }
}
