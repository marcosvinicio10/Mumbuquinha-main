using UnityEngine;

public class MoedaScript : MonoBehaviour
{
    public ContadorMoedas count;
    public AudioClip somMoeda;         // arraste o som aqui no Inspector
    private AudioSource audioSource;   // referência interna ao AudioSource

    public void Start()
    {
        count = FindAnyObjectByType<ContadorMoedas>();
        audioSource = GetComponent<AudioSource>(); // pega o AudioSource da moeda
    }

    private void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pegou a moeda!");

            // toca o som antes de desativar
            audioSource.PlayOneShot(somMoeda);

            count.moedas++;

            // desativa a moeda depois do som
            StartCoroutine(DesativarDepoisDoSom());
        }
    }

    private System.Collections.IEnumerator DesativarDepoisDoSom()
    {
        yield return new WaitForSeconds(somMoeda.length);
        gameObject.SetActive(false);
    }
}
