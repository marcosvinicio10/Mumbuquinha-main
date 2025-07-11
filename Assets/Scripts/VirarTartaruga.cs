using UnityEngine;
using TMPro;

public class VirarTartaruga : MonoBehaviour
{
    public GameObject tartaruga;
    public GameObject EButton; // Este botão deve estar como filho deste objeto
    public TextMeshProUGUI textoUI;
    public GameObject objetoParaAtivar;
    public GameObject outroObjetoParaAtivar;

    private bool podeVirar = false;
    private bool jaVirou = false;

    void Start()
    {
        if (EButton != null)
            EButton.SetActive(false); // Desativa só o botão desta instância

        if (textoUI != null)
            textoUI.gameObject.SetActive(false);

        if (objetoParaAtivar != null)
            objetoParaAtivar.SetActive(false);

        if (outroObjetoParaAtivar != null)
            outroObjetoParaAtivar.SetActive(false);
    }

    void Update()
    {
        if (podeVirar && !jaVirou && Input.GetKeyDown(KeyCode.E))
        {
            if (tartaruga != null)
                tartaruga.transform.Rotate(180f, 0f, 0f);

            if (objetoParaAtivar != null)
                objetoParaAtivar.SetActive(true);

            if (outroObjetoParaAtivar != null)
                outroObjetoParaAtivar.SetActive(true);

            if (EButton != null)
                EButton.SetActive(false);

            if (textoUI != null)
                textoUI.gameObject.SetActive(false);

            jaVirou = true;
            podeVirar = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jaVirou)
        {
            if (EButton != null)
                EButton.SetActive(true);

            if (textoUI != null)
                textoUI.gameObject.SetActive(true);

            podeVirar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (EButton != null)
                EButton.SetActive(false);

            if (textoUI != null)
                textoUI.gameObject.SetActive(false);

            podeVirar = false;
        }
    }
}