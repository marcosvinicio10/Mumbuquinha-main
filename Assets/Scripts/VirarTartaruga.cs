using UnityEngine;
using TMPro;

public class VirarTartaruga : MonoBehaviour
{
    public GameObject tartaruga; // Objeto que vai virar
    public TextMeshProUGUI textoUI; // Texto que aparece ao encostar
    public GameObject objetoParaAtivar; // Novo objeto que será ativado ao pressionar L

    private bool podeVirar = false;
    private bool jaVirou = false; // Novo controle para impedir múltiplas ativações

    void Start()
    {
        if (textoUI != null)
        {
            textoUI.gameObject.SetActive(false); // Esconde o texto no início
        }

        if (objetoParaAtivar != null)
        {
            objetoParaAtivar.SetActive(false); // Garante que começa desativado
        }
    }

    void Update()
    {
        if (podeVirar && !jaVirou && Input.GetKeyDown(KeyCode.L))
        {
            if (tartaruga != null)
            {
                tartaruga.transform.Rotate(180f, 0f, 0f); // Vira a tartaruga no eixo X
            }

            if (objetoParaAtivar != null)
            {
                objetoParaAtivar.SetActive(true); // Ativa o novo objeto
            }

            if (textoUI != null)
            {
                textoUI.gameObject.SetActive(false); // Esconde o texto
            }

            jaVirou = true;    // Marca que já virou
            podeVirar = false; // Impede novas ativações
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && textoUI != null && !jaVirou)
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