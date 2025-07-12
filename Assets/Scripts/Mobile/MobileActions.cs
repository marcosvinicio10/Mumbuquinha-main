using UnityEngine;

public class MobileActions : MonoBehaviour
{
    public void Pular()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            var mov = playerObj.GetComponent<PlayerMovement3DMobile>();
            mov?.Pular();
        }
    }

    public void Dash()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            var mov = playerObj.GetComponent<PlayerMovement3DMobile>();
            mov?.RealizarDash();
        }
    }

    public void Interagir()
    {
        Debug.Log("Interagiu! (bot�o de intera��o acionado)");
        // Aqui voc� coloca a l�gica quando tiver algo para interagir
    }
}
