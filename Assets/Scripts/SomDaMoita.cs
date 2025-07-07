using UnityEngine;

public class SomDaMoita : MonoBehaviour
{
    public AudioClip somMoita;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && somMoita != null)
        {
            audioSource.PlayOneShot(somMoita);
        }
    }
}
