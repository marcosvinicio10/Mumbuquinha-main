using UnityEngine;

public class DanoScript : MonoBehaviour
{
    private ControladorDEVida vidaDoPlayer;

    private void Start()
    {
        vidaDoPlayer = FindAnyObjectByType<ControladorDEVida>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player levou dano!");

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(1); // Ajuste o valor do dano aqui
            }

            // Se quiser, desative o objeto que causou dano
            // gameObject.SetActive(false);
        }
    }
}
