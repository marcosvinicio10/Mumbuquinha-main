using UnityEngine;
using UnityEngine.Video;

public class VidaPickup : MonoBehaviour
{
    public int quantidade = 1;
    public AudioClip somCoracao; // Som ao pegar o coração
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(somCoracao);
            VidaManager vm = FindFirstObjectByType<VidaManager>();
            if (vm != null)
            {
                vm.Curar(quantidade);
                Destroy(gameObject);
            }
        }
    }
}
