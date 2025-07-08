using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    // ————— VARIÁVEL COMPARTILHADA ENTRE CENAS —————
    public static string selectedCharacter;
    public string nomeDaCena;

    // ————— FUNÇÃO CHAMADA PELOS BOTÕES —————
    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;
        SceneManager.LoadScene(nomeDaCena);
    }
}