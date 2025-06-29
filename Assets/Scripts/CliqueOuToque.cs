using UnityEngine;
using UnityEngine.SceneManagement;


public class CliqueOuToque : MonoBehaviour
{

    public string nomeDaCena;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) ||
           (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            SceneManager.LoadScene(nomeDaCena);

        }
    }
}
