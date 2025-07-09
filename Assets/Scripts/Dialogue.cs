using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("Referências de UI")]
    public TMP_Text nomeTexto;
    public TMP_Text falaTexto;
    public GameObject caixaDeMensagem;
    public TextMeshProUGUI textoInteracao; // NOVO: texto "Pressione E"

    [Header("Dados do NPC")]
    public string nomeDoNPC = "NPC";
    [TextArea(2, 5)]
    public List<string> falas;
    [TextArea(2, 5)]
    public string falaFinal = "Foi bom conversar com você!";

    [Header("Configurações de Digitação")]
    public float velocidadeDigitacao = 0.05f;
    public float delayInicial = 0.5f;

    [Header("Estado do Diálogo")]
    public bool dialogoConcluido = false;

    private int indexFalaAtual = 0;
    private bool digitando = false;
    private Coroutine coroutineAtual;
    private bool jogadorPerto = false;
    private bool dialogoAtivo = false;

    void Start()
    {
        nomeTexto.text = nomeDoNPC;
        falaTexto.text = "";
        caixaDeMensagem.SetActive(false);

        if (textoInteracao != null)
            textoInteracao.gameObject.SetActive(false);
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoAtivo)
            {
                if (textoInteracao != null)
                    textoInteracao.gameObject.SetActive(false); // Oculta aviso

                IniciarDialogo();
            }
            else if (!digitando)
            {
                AvancarFala();
            }
        }
    }

    public void IniciarDialogo()
    {
        indexFalaAtual = 0;
        dialogoAtivo = true;
        caixaDeMensagem.SetActive(true);
        nomeTexto.text = nomeDoNPC;

        if (dialogoConcluido)
        {
            if (coroutineAtual != null) StopCoroutine(coroutineAtual);
            coroutineAtual = StartCoroutine(DigitarFala(falaFinal));
        }
        else
        {
            AvancarFala();
        }
    }

    private void AvancarFala()
    {
        if (dialogoConcluido)
        {
            if (coroutineAtual != null) StopCoroutine(coroutineAtual);
            coroutineAtual = StartCoroutine(DigitarFala(falaFinal));
            return;
        }

        if (indexFalaAtual < falas.Count)
        {
            if (coroutineAtual != null) StopCoroutine(coroutineAtual);
            coroutineAtual = StartCoroutine(DigitarFala(falas[indexFalaAtual]));
            indexFalaAtual++;
        }
        else
        {
            dialogoAtivo = false;
            caixaDeMensagem.SetActive(false);
        }
    }

    IEnumerator DigitarFala(string frase)
    {
        digitando = true;
        falaTexto.text = "";

        yield return new WaitForSeconds(delayInicial);

        foreach (char letra in frase)
        {
            falaTexto.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        digitando = false;
    }

    public void ConcluirDialogo()
    {
        dialogoConcluido = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;

            if (textoInteracao != null)
                textoInteracao.gameObject.SetActive(true); // MOSTRA aviso
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            dialogoAtivo = false;
            caixaDeMensagem.SetActive(false);

            if (textoInteracao != null)
                textoInteracao.gameObject.SetActive(false); // ESCONDE aviso
        }
    }
}
