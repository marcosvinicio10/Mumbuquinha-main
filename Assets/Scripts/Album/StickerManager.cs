using UnityEngine;
using System.Collections;

public class StickerManager : MonoBehaviour
{
    [System.Serializable]
    public class SlotFigurinha
    {
        public Transform silhueta;         // Local onde a figurinha vai aparecer
        public GameObject figurinhaPrefab; // Prefab da figurinha coletável
    }

    [Header("Figurinhas e Álbum")]
    public SlotFigurinha[] figurinhas;
    public Transform albumPai; // Pai geral do álbum (Canvas ou painel)

    private bool[] figurinhasColetadas;

    void Start()
    {
        figurinhasColetadas = new bool[figurinhas.Length];
    }

    public void ColetarFigurinha(int id)
    {
        if (id < 0 || id >= figurinhas.Length) return;
        if (figurinhasColetadas[id]) return; // já foi coletada

        figurinhasColetadas[id] = true;

        var slot = figurinhas[id];

        if (slot.figurinhaPrefab != null && slot.silhueta != null)
        {
            // Instancia a figurinha como filha da silhueta para garantir escala certa
            GameObject nova = Instantiate(slot.figurinhaPrefab);
            RectTransform novaRT = nova.GetComponent<RectTransform>();
            RectTransform silhuetaRT = slot.silhueta.GetComponent<RectTransform>();

            // Define a silhueta como pai e mantém alinhamento de UI
            novaRT.SetParent(silhuetaRT, false);

            // Centraliza e ajusta escala/tamanho
            novaRT.anchorMin = silhuetaRT.anchorMin;
            novaRT.anchorMax = silhuetaRT.anchorMax;
            novaRT.pivot = silhuetaRT.pivot;
            novaRT.anchoredPosition = Vector2.zero;
            novaRT.sizeDelta = silhuetaRT.sizeDelta;
            novaRT.localRotation = silhuetaRT.localRotation;
            novaRT.localScale = Vector3.zero; // começa pequeno para animar

            StartCoroutine(AnimarAparicao(novaRT));
        }
    }

    IEnumerator AnimarAparicao(Transform obj)
    {
        float duracao = 0.4f;
        float tempo = 0f;

        while (tempo < duracao)
        {
            float t = tempo / duracao;
            obj.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            tempo += Time.deltaTime;
            yield return null;
        }

        obj.localScale = Vector3.one;
    }
}
