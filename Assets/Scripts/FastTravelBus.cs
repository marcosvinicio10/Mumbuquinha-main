using UnityEngine;

public class FastTravelBus : MonoBehaviour
{
    [Tooltip("Defina se esse é o ônibus central (ponto de destino)")]
    public bool isCentralBus = false;

    private static Transform centralBusTransform;
    private bool isPlayerNear = false;
    private GameObject playerInTrigger = null;
    private bool podeViajar = true; // controla se o jogador pode viajar agora

    void Start()
    {
        if (isCentralBus)
        {
            centralBusTransform = this.transform;
            Debug.Log("Central Bus definido: " + name);
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            TryFastTravel();
        }
    }

    public void TryFastTravel()
    {
        if (!podeViajar)
            return;

        if (isCentralBus)
        {
            Debug.Log("Esse é o ônibus central, não pode viajar.");
            return;
        }

        if (centralBusTransform == null)
        {
            Debug.LogWarning("Ônibus central não encontrado!");
            return;
        }

        if (playerInTrigger != null)
        {
            CharacterController controller = playerInTrigger.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;

            Vector3 destino = centralBusTransform.position + Vector3.up * 1f;
            playerInTrigger.transform.position = destino;

            if (controller != null)
                controller.enabled = true;

            // Reseta os estados para evitar teleportes consecutivos
            isPlayerNear = false;
            playerInTrigger = null;
            podeViajar = false;

            Debug.Log("Player teleportado para: " + destino);
        }
        else
        {
            Debug.LogWarning("Player não encontrado na trigger!");
        }
    }

    // Para uso via botão mobile
    public void FastTravelFromButton()
    {
        if (isPlayerNear)
        {
            TryFastTravel();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entrou no ônibus: " + name);
            isPlayerNear = true;
            playerInTrigger = other.gameObject;
            podeViajar = true; // libera viagem quando entra no trigger
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject == playerInTrigger)
        {
            Debug.Log("Player saiu do ônibus: " + name);
            isPlayerNear = false;
            playerInTrigger = null;
            podeViajar = true; // libera viagem para próxima entrada
        }
    }
}