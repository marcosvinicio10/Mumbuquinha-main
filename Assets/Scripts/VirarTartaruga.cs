using UnityEngine;
using TMPro;

public class VirarTartaruga : MonoBehaviour
{
    public GameObject tartaruga; // Objeto que vai virar
    public TextMeshProUGUI textoUI; // Texto que aparece ao encostar
    public GameObject objetoParaAtivar; // Novo objeto que ser� ativado ao pressionar L

    private bool podeVirar = false;
    private bool jaVirou = false; // Novo controle para impedir m�ltiplas ativa��es

    void Start()
    {
        if (textoUI != null)
        {
            textoUI.gameObject.SetActive(false); // Esconde o texto no in�cio
        }

        if (objetoParaAtivar != null)
        {
            objetoParaAtivar.SetActive(false); // Garante que come�a desativado
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

            jaVirou = true;    // Marca que j� virou
            podeVirar = false; // Impede novas ativa��es
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