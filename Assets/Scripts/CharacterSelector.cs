using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    //  VARIมVEL COMPARTILHADA ENTRE CENAS 
    public static string selectedCharacter;
    public string nomeDaCena;

    //  FUNวรO CHAMADA PELOS BOTีES 
    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;
        SceneManager.LoadScene(nomeDaCena);
    }
}