using UnityEngine;

public class MoedaScript : MonoBehaviour
{
    public ContadorMoedas count;

    public void Start()
    {
        count = FindAnyObjectByType<ContadorMoedas>();
    }
    private void Update()
    {
        transform.Rotate (0, 90 * Time.deltaTime, 0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colidiu com" + other.name);

            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Pegou a moeda!");
            count.moedas++;
        }
    }

}
