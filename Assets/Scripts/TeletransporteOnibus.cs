using UnityEngine;

public class TeleporteOnibus : MonoBehaviour
{
    [Header("Destino do Teleporte")]
    public Transform destino;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && destino != null)
        {
            other.transform.position = destino.position;
            Debug.Log("Player teleportado para: " + destino.name);
        }
    }
}
