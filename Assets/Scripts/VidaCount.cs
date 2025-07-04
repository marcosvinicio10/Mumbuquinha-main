using UnityEngine;
using TMPro;

public class VidaCount : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 100;
    private int vidaAtual;

    [Header("Interface")]
    public TextMeshProUGUI textoVida;

    [Header("Dano")]
    public int danoPorContato = 10;
    public string tagObjetoQueCausaDano = "Dano"; // objetos com essa tag vão causar dano

    void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarTextoVida();
    }

    void Update()
    {
        // só pra teste: reinicia a vida com a tecla R
        if (Input.GetKeyDown(KeyCode.R))
        {
            vidaAtual = vidaMaxima;
            AtualizarTextoVida();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // verifica se o objeto tocado tem a tag de dano
        if (other.CompareTag(tagObjetoQueCausaDano))
        {
            Debug.Log("Tocou em objeto de dano: " + other.name);
            TomarDano(danoPorContato);

            // Se quiser desativar o objeto que causou dano (tipo uma armadilha descartável)
            // other.gameObject.SetActive(false);
        }
    }

    void TomarDano(int dano)
    {
        vidaAtual -= dano;
        vidaAtual = Mathf.Max(0, vidaAtual);
        AtualizarTextoVida();

        Debug.Log("Tomou dano! Vida atual: " + vidaAtual);

        if (vidaAtual <= 0)
        {
            Debug.Log("Player morreu!");
            // Aqui você pode chamar uma animação, reiniciar cena, etc.
        }
    }

    void AtualizarTextoVida()
    {
        if (textoVida != null)
            textoVida.text = "Vida: " + vidaAtual.ToString();
    }
}
