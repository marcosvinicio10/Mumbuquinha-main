using TMPro;
using UnityEngine;

public class ContadorMoedas : MonoBehaviour
{
    public int moedas;
    public TextMeshProUGUI texto;

    public void Update()
    {
       texto.text = moedas.ToString();
    }
}
