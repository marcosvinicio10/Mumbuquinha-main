using UnityEngine;

public class Video : MonoBehaviour
{
    public float tempoExibicao = 9f;

    void Start()
    {
        Invoke("DesativarVideo", tempoExibicao);
    }

    void DesativarVideo()
    {
        gameObject.SetActive(false);
    }
}
