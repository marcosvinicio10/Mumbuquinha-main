using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ControladorDEVida : MonoBehaviour
{
    [Header("Vida do jogador")]
    public int vida = 5;
    public TextMeshProUGUI textoVida;

    [Header("Checkpoints")]
    public List<Transform> checkpoints;
    private Transform checkpointAtual;

    [Header("Checkpoint padrão")]
    public Transform checkpointInicial;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        AtualizarVida();
    }

    public void TomarDano(int dano)
    {
        vida -= dano;
        vida = Mathf.Max(vida, 0);
        AtualizarVida();

        if (vida <= 0)
        {
            Morrer();
        }
    }

    public void CurarVida(int quantidade)
    {
        vida += quantidade;
        vida = Mathf.Min(vida, 5);
        AtualizarVida();
    }

    public void DefinirCheckpoint(Transform novoCheckpoint)
    {
        checkpointAtual = novoCheckpoint;
        Debug.Log("Novo checkpoint definido: " + novoCheckpoint.name);
    }

    void Morrer()
    {
        Debug.Log("Player morreu!");

        Transform destino = checkpointAtual != null ? checkpointAtual : checkpointInicial;
        TeleportarPara(destino);

        vida = 5;
        AtualizarVida();
    }

    void AtualizarVida()
    {
        if (textoVida != null)
        {
            textoVida.text = "Vida: " + vida;
        }
    }

    public void TocarNaAgua()
    {
        vida -= 1;
        AtualizarVida();

        if (vida <= 0)
        {
            Morrer();
        }
        else
        {
            Debug.Log("Retornando ao checkpoint por água...");
            Transform destino = checkpointAtual != null ? checkpointAtual : checkpointInicial;
            TeleportarPara(destino);
        }
    }

    void TeleportarPara(Transform destino)
    {
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = destino.position;
            controller.enabled = true;
        }
        else
        {
            transform.position = destino.position;
        }

        Debug.Log("Teleportado para: " + destino.name);
    }
}
