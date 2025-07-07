using UnityEngine;

public class DanoScript : MonoBehaviour
{
    private ControladorDEVida vidaDoPlayer;
    private AudioSource audioSource;

    [Header("Som de Dano")]
    public AudioClip somDano;

    private void Start()
    {
        vidaDoPlayer = FindAnyObjectByType<ControladorDEVida>();
        audioSource = GetComponent<AudioSource>(); // pega o AudioSource no mesmo objeto
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player levou dano!");

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.TomarDano(1); // Aplica o dano
            }

            if (audioSource != null && somDano != null)
            {
                audioSource.PlayOneShot(somDano); // Toca o som
            }

            // Se quiser, desativa o objeto depois do dano
            // gameObject.SetActive(false);
        }
    }
}
