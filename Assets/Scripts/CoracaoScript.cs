using UnityEngine;

public class CoracaoScript : MonoBehaviour
{
    private ControladorDEVida vidaDoPlayer;

    public int cura = 1; // Valor que ser� recuperado
    public AudioClip somCoracao; // Som ao pegar o cora��o
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
            Debug.Log("Pegou um cora��o!");

            if (vidaDoPlayer != null)
            {
                vidaDoPlayer.CurarVida(cura);
            }

            if (audioSource != null && somCoracao != null)
            {
                audioSource.PlayOneShot(somCoracao);
            }

            // Desativa o cora��o depois de tocar o som
            StartCoroutine(DesativarDepoisDoSom());
        }
    }

    private System.Collections.IEnumerator DesativarDepoisDoSom()
    {
        yield return new WaitForSeconds(somCoracao.length);
        gameObject.SetActive(false);
    }
}
