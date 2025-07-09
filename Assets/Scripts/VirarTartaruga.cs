using UnityEngine;
using TMPro;

public class VirarTartaruga : MonoBehaviour
{
    public GameObject tartaruga; // Objeto que vai virar
    public TextMeshProUGUI textoUI; // Texto que aparece ao encostar
    private bool podeVirar = false;

    void Start()
    {
        if (textoUI != null)
        {
            textoUI.gameObject.SetActive(false); // Esconde o texto no início
        }
    }

    void Update()
    {
        if (podeVirar && Input.GetKeyDown(KeyCode.L))
        {
            if (tartaruga != null)
            {
                tartaruga.transform.Rotate(180f, 0f, 0f); // Vira a tartaruga no eixo X
                textoUI.gameObject.SetActive(false); // Esconde o texto
                podeVirar = false; // Só vira uma vez
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textoUI != null)
        {
            textoUI.gameObject.SetActive(true);
            podeVirar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && textoUI != null)
        {
            textoUI.gameObject.SetActive(false);
            podeVirar = false;
        }
    }
}
