using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class DanoTrigger : MonoBehaviour
{
    public int dano = 1;
    public float delayEntreDanos = 0.5f;
    private bool podeDarDano = true;
    public AudioClip somDano;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && podeDarDano)
        {
            StartCoroutine(AplicarDanoComDelay());

            VidaManager vm = FindFirstObjectByType<VidaManager>();
            if (vm != null)
            {
                vm.DarDano(dano);
            }

            DynamicCamera camera = FindFirstObjectByType<DynamicCamera>();
            if (camera != null)
            {
                camera.IniciarTremor();
            }
        }
    }

    IEnumerator AplicarDanoComDelay()
    {
        audioSource.PlayOneShot(somDano);
        podeDarDano = false;
        yield return new WaitForSeconds(delayEntreDanos);
        podeDarDano = true;
    }
}
