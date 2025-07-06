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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }


    void Update()
    {
    
        float deslocamentoX = Mathf.Sin(Time.time * velocidade) * amplitude;

  
        transform.position = new Vector3(posicaoInicial.x + deslocamentoX, posicaoInicial.y, posicaoInicial.z);
    }
}
