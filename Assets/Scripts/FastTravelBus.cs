using UnityEngine;

public class FastTravelBus : MonoBehaviour
{
    [Tooltip("Defina se esse � o �nibus central (ponto de destino)")]
    public bool isCentralBus = false;

    private static Transform centralBusTransform;
    private bool isPlayerNear;

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
        if (isCentralBus)
        {
            Debug.Log("Esse � o �nibus central, n�o pode viajar.");
            return;
        }

        if (centralBusTransform == null)
        {
            Debug.LogWarning("�nibus central n�o encontrado!");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            // Pega o CharacterController do player
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
                controller.enabled = false;

            // Define destino com um pequeno offset pra evitar bugs de colis�o no ch�o
            Vector3 destino = centralBusTransform.position + Vector3.up * 1f;
            player.transform.position = destino;

            if (controller != null)
                controller.enabled = true;

            Debug.Log("Player teleportado para: " + destino);
        }
        else
        {
            Debug.LogWarning("Player n�o encontrado na cena!");
        }
    }

    // Para uso via bot�o mobile
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
            Debug.Log("Player entrou no �nibus!");
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
