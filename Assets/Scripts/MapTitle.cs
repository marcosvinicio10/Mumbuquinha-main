using UnityEngine;

public class MapTitle : MonoBehaviour
{
    public GameObject titleUI; // arraste aqui sua imagem ou painel da UI no Inspector

    float titleTime = 0f;
    bool titleActive = false;

    void Update()
    {
        if (titleActive)
        {
            titleTime += Time.deltaTime;

            if (titleTime > 5f)
            {
                titleUI.SetActive(false);
                titleActive = false;
                titleTime = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            titleUI.SetActive(true);
            titleActive = true;
            titleTime = 0f;
        }
    }
}