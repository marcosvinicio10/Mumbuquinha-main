using UnityEngine;

public class PlataformaQueCai : MonoBehaviour
{
    public float tempoParaCair = 1.5f;
    public float alturaRaycast = 1f; // altura que vai olhar acima da plataforma
    public string tagDoPlayer = "Player";

    private bool ativado = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    void Update()
    {
        if (ativado) return;

        RaycastHit hit;
        Vector3 origem = transform.position + Vector3.up * alturaRaycast;

        if (Physics.Raycast(origem, Vector3.down, out hit, alturaRaycast + 0.2f))
        {
            if (hit.collider.CompareTag(tagDoPlayer))
            {
                ativado = true;
                Invoke("AtivarQueda", tempoParaCair);
            }
        }
    }

    void AtivarQueda()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}
