using UnityEngine;
using System.Collections;
using TMPro;

public class VidaManager : MonoBehaviour
{
    public int vidaMaxima = 5;
    public int vidaAtual = 5;

    [Header("UI")]
    public TextMeshProUGUI textoVida;

    [Header("Referência dinâmica")]
    private GameObject player;
    private Transform ultimoCheckpoint;
    public Transform checkpointInicial;

    private CharacterController controller;

    void Start()
    {
        StartCoroutine(EncontrarPlayerCoroutine());
        Invoke("EncontrarPlayer", 0.5f);
        vidaAtual = vidaMaxima;
        AtualizarHUD();
    }

    void EncontrarPlayer()
    {
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            controller = player.GetComponent<CharacterController>();
            if (checkpointInicial != null)
                ultimoCheckpoint = checkpointInicial;
        }
        else
        {
            Debug.LogWarning("Nenhum objeto com tag Player encontrado!");
        }
    }

    public void DarDano(int dano)
    {
        if (player == null) EncontrarPlayer();

        vidaAtual -= dano;
        vidaAtual = Mathf.Max(0, vidaAtual);
        AtualizarHUD();

        if (vidaAtual <= 0)
            Morrer();
    }

    public void Curar(int quantidade)
    {
        if (player == null) EncontrarPlayer();

        vidaAtual += quantidade;
        vidaAtual = Mathf.Min(vidaAtual, vidaMaxima);
        AtualizarHUD();
    }

    public void DefinirCheckpoint(Transform novo)
    {
        ultimoCheckpoint = novo;
    }

    void Morrer()
    {
        if (player == null) EncontrarPlayer();

        Debug.Log("Morreu! Respawn...");
        Transform destino = ultimoCheckpoint != null ? ultimoCheckpoint : checkpointInicial;
        StartCoroutine(Teleportar(destino));

        vidaAtual = vidaMaxima;
        AtualizarHUD();
    }

    IEnumerator Teleportar(Transform destino)
    {
        if (player != null && controller != null)
        {
            controller.enabled = false;
            yield return null;
            player.transform.position = destino.position;
            controller.enabled = true;
        }
        else if (player != null)
        {
            player.transform.position = destino.position;
        }
    }
    IEnumerator EncontrarPlayerCoroutine()
    {
        while (player == null)
        {
            player = GameObject.FindWithTag("Player");

            if (player != null)
            {
                controller = player.GetComponent<CharacterController>();
                if (checkpointInicial != null)
                    ultimoCheckpoint = checkpointInicial;
                yield break;
            }

            yield return null; // espera 1 frame e tenta de novo
        }
    }

    void AtualizarHUD()
    {
        if (textoVida != null)
            textoVida.text = vidaAtual.ToString();
    }
    public void MorteInstantanea()
    {
        vidaAtual = 0;
        AtualizarHUD();
        Morrer();
    }
}
