using UnityEngine;
using TMPro;

public class CapivaraEFigurinha : MonoBehaviour
{
    public GameObject objeto1; // Primeiro objeto a ativar
    public GameObject objeto2; // Segundo objeto a ativar
    public TextMeshProUGUI textoUI; // Texto para mostrar na tela
    private bool jaAtivou = false;

    void Start()
    {
        // Garante que tudo começa desativado
        if (objeto1 != null) objeto1.SetActive(false);
        if (objeto2 != null) objeto2.SetActive(false);
        if (textoUI != null) textoUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!jaAtivou && other.CompareTag("Player"))
        {
            if (objeto1 != null) objeto1.SetActive(true);
            if (objeto2 != null) objeto2.SetActive(true);
            if (textoUI != null) textoUI.gameObject.SetActive(true);

            jaAtivou = true;
        }
    }
}
