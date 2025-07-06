using UnityEngine;

public class Checkpoint : MonoBehaviour
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
                player.DefinirCheckpoint(transform);
                Debug.Log("Checkpoint ativado: " + gameObject.name);
            }

          
        }
    }
}
