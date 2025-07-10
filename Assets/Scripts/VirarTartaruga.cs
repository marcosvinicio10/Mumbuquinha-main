using UnityEngine;
using TMPro;

public class VirarTartaruga : MonoBehaviour
{
    public GameObject tartaruga; // Objeto que vai virar
    public TextMeshProUGUI textoUI; // Texto que aparece ao encostar
    public GameObject objetoParaAtivar; // Objeto que será ativado ao pressionar L
    public GameObject outroObjetoParaAtivar; // Novo objeto adicional que será ativado

    private bool podeVirar = false;
    private bool jaVirou = false; // Impede múltiplas ativações

    void Start()
    {
        if (textoUI != null)
        {
            textoUI.gameObject.SetActive(false);
        }

        if (objetoParaAtivar != null)
        {
            objetoParaAtivar.SetActive(false);
        }

        if (outroObjetoParaAtivar != null)
        {
            outroObjetoParaAtivar.SetActive(false); // Garante que começa desativado
        }
    }

    void Update()
    {
        if (podeVirar && !jaVirou && Input.GetKeyDown(KeyCode.L))
        {
            if (tartaruga != null)
            {
                tartaruga.transform.Rotate(180f, 0f, 0f);
            }

            if (objetoParaAtivar != null)
            {
                objetoParaAtivar.SetActive(true);
            }

            if (outroObjetoParaAtivar != null)
            {
                outroObjetoParaAtivar.SetActive(true); // Ativa o novo objeto adicional
            }

            if (textoUI != null)
            {
                textoUI.gameObject.SetActive(false);
            }

            jaVirou = true;
            podeVirar = false;
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
