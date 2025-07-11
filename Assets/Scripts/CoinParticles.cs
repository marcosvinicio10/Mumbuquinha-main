using UnityEngine;

public class CoinParticles : MonoBehaviour
{
    public ParticleSystem sistemaParticulas;

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag("Player"))
        {
            Debug.Log("Soltou Particulas");
            sistemaParticulas.Play();
        }
    }
}
