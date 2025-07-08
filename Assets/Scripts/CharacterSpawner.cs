using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [System.Serializable]
    public class CharacterOption
    {
        public string name;
        public GameObject prefab;
    }

    public CharacterOption[] characters;

    void Start()
    {
        string selected = CharacterSelector.selectedCharacter;

        if (string.IsNullOrEmpty(selected))
        {
            Debug.LogError("Nenhum personagem selecionado.");
            return;
        }

        foreach (CharacterOption option in characters)
        {
            if (option.name == selected)
            {
                GameObject player = Instantiate(option.prefab, transform.position, transform.rotation);
                player.tag = "Player";
                return;
            }
        }

        Debug.LogError("Personagem não encontrado na lista.");
    }
}