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
        if (isCentralBus || centralBusTransform == null)
            return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = centralBusTransform.position;
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
