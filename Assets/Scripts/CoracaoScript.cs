using UnityEngine;

public class CoracaoScript : MonoBehaviour
{
    private ControladorDEVida vidaDoPlayer;

    public int cura = 1; // Valor que ser� recuperado

    void Start()
    {
        vidaDoPlayer = FindAnyObjectByType<ControladorDEVida>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pegou um cora��o!");

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.CurarVida(cura);
            }

            gameObject.SetActive(false);
        }
    }
}
