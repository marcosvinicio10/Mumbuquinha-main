using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    // ����� VARI�VEL COMPARTILHADA ENTRE CENAS �����
    public static string selectedCharacter;
    public string nomeDaCena;

    // ����� FUN��O CHAMADA PELOS BOT�ES �����
    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;
        SceneManager.LoadScene(nomeDaCena);
    }
}