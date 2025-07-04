using UnityEngine;

public class SenoideM : MonoBehaviour
{
    public float amplitude = 2f;     
    public float velocidade = 2f;
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.position;
    }

    void Update()
    {
    
        float deslocamentoX = Mathf.Sin(Time.time * velocidade) * amplitude;

  
        transform.position = new Vector3(posicaoInicial.x + deslocamentoX, posicaoInicial.y, posicaoInicial.z);
    }
}
