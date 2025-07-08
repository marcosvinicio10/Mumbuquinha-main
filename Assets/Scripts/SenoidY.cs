using UnityEngine;

public class SenoidY : MonoBehaviour
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

        float deslocamentoY = Mathf.Sin(Time.time * velocidade) * amplitude;


        transform.position = new Vector3(posicaoInicial.x , posicaoInicial.y + deslocamentoY, posicaoInicial.z);
    }
}
