using UnityEngine;

public class StickerAlbumUI : MonoBehaviour
{
    // ————— REFERÊNCIA DO ÁLBUM —————
    public GameObject painelAlbum;

    // ————— BOTÃO CHAMANDO ESSA FUNÇÃO —————
    void Update()
    {
        AlternarAlbumII();
    }
    public void AlternarAlbum()
    {
        painelAlbum.SetActive(!painelAlbum.activeSelf);
    }
    public void AlternarAlbumII()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            painelAlbum.SetActive(!painelAlbum.activeSelf);
        }
    }
}
