using UnityEngine;

public class AguaScript : MonoBehaviour
{
    private ControladorDEVida player;

    void Start()
    {
        player = FindAnyObjectByType<ControladorDEVida>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                Debug.Log("Player caiu na  gua!");
                player.TocarNaAgua();
            }
        }
    }
}