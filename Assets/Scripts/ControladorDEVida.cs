using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ControladorDEVida : MonoBehaviour
{
    [Header("Vida do jogador")]
    public int vida = 5;
    public TextMeshProUGUI textoVida;

    [Header("Checkpoints")]
    public List<Transform> checkpoints; // Lista p blica no Inspector
    private Transform checkpointAtual;

    [Header("Checkpoint padrão")]
    public Transform checkpointInicial;


    void Start()
    {
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

        if (checkpointAtual != null)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true; // desativa f sica temporariamente
            }

            transform.position = checkpointAtual.position;

            if (rb != null)
                rb.isKinematic = false;

            vida = 5;
            AtualizarVida();

            Debug.Log("Teleportando para checkpoint: " + checkpointAtual.position);

        }
        else
        {
            Debug.LogWarning("Nenhum checkpoint definido.");
            transform.position = checkpointInicial.position;
        }
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
            if (checkpointAtual != null)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }

                transform.position = checkpointAtual.position;

                if (rb != null)
                    rb.isKinematic = false;
            }
            else
            {
                Debug.LogWarning("Sem checkpoint salvo.");
                transform.position = checkpointInicial.position;

            }
        }
    }

}