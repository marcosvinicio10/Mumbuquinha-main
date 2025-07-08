using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // ————— REFERÊNCIAS E CONFIGURAÇÕES —————
    [Header("Referências de UI")]
    public TMP_Text nomeTexto;
    public TMP_Text falaTexto;
    public GameObject caixaDeMensagem;

    [Header("Dados do NPC")]
    public string nomeDoNPC = "NPC";
    [TextArea(2, 5)]
    public List<string> falas;
    [TextArea(2, 5)]
    public string falaFinal = "Ah, foi muito bom ter conversado com você!";

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
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogoAtivo)
            {
                IniciarDialogo();
            }
            else if (!digitando)
            {
                AvancarFala();
            }
        }
    }

    // ————— FUNÇÕES DE DIÁLOGO —————

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
            // Repete só a fala final
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
            dialogoAtivo = false; // Encerra o diálogo principal
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

    // ————— DIÁLOGO APÓS CONCLUIR MISSÃO —————

    public void ConcluirDialogo()
    {
        dialogoConcluido = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            caixaDeMensagem.SetActive(false);
            dialogoAtivo = false;
        }
    }
}