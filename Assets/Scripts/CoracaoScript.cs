using UnityEngine;

public class CoracaoScript : MonoBehaviour
{
    private ControladorDEVida vidaDoPlayer;

    public int cura = 1; // Valor que será recuperado

    void Start()
    {
        vidaDoPlayer = FindAnyObjectByType<ControladorDEVida>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pegou um coração!");

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.CurarVida(cura);
            }

            gameObject.SetActive(false);
        }
    }
}
