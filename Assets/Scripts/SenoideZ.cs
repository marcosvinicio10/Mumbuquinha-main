using UnityEngine;

public class SenoideZ : MonoBehaviour
{
    public float amplitude = 2f;     // Quanto oscila
    public float velocidade = 2f;    // Quão rápido oscila
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
        float deslocamentoZ = Mathf.Sin(Time.time * velocidade) * amplitude;

        transform.position = new Vector3(posicaoInicial.x, posicaoInicial.y, posicaoInicial.z + deslocamentoZ);
    }
}
