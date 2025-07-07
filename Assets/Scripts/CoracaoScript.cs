using UnityEngine;

public class CoracaoScript : MonoBehaviour
{
    private ControladorDEVida vidaDoPlayer;

    public int cura = 1; // Valor que será recuperado
    public AudioClip somCoracao; // Som ao pegar o coração
    private AudioSource audioSource;

    void Start()
    {
        vidaDoPlayer = FindAnyObjectByType<ControladorDEVida>();
        audioSource = GetComponent<AudioSource>();
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

            if (audioSource != null && somCoracao != null)
            {
                audioSource.PlayOneShot(somCoracao);
            }

            // Desativa o coração depois de tocar o som
            StartCoroutine(DesativarDepoisDoSom());
        }
    }

    private System.Collections.IEnumerator DesativarDepoisDoSom()
    {
        yield return new WaitForSeconds(somCoracao.length);
        gameObject.SetActive(false);
    }
}
