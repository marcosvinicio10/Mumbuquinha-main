using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StickerDialogUI : MonoBehaviour
{
    public GameObject painelDialogo;
    public TextMeshProUGUI textoDialogo;
    public Image imagemFigurinha;

    public float velocidadeDigitacao = 0.05f;
    public float tempoExtraAntesDeFechar = 1.5f;
    public Vector3 escalaMin = Vector3.one;
    public Vector3 escalaMax = new Vector3(1.1f, 1.1f, 1.1f);
    public float velocidadePulsar = 2f;

    private Coroutine animacaoAtual;

    public void MostrarFigurinha(Sprite sprite, string texto)
    {
        StopAllCoroutines();
        painelDialogo.SetActive(true);
        imagemFigurinha.sprite = sprite;
        imagemFigurinha.transform.localScale = escalaMin;
        textoDialogo.text = "";

        animacaoAtual = StartCoroutine(PulsarImagem());
        StartCoroutine(MostrarTextoDigitado(texto));
    }

    IEnumerator MostrarTextoDigitado(string texto)
    {
        foreach (char c in texto)
        {
            textoDialogo.text += c;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        yield return new WaitForSeconds(tempoExtraAntesDeFechar);
        FecharDialogo();
    }

    IEnumerator PulsarImagem()
    {
        while (true)
        {
            float t = Mathf.PingPong(Time.time * velocidadePulsar, 1f);
            imagemFigurinha.transform.localScale = Vector3.Lerp(escalaMin, escalaMax, t);
            yield return null;
        }
    }

    public void FecharDialogo()
    {
        if (animacaoAtual != null)
            StopCoroutine(animacaoAtual);

        painelDialogo.SetActive(false);
    }
}
