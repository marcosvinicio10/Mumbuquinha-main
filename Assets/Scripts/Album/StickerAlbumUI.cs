using UnityEngine;

public class StickerAlbumUI : MonoBehaviour
{
    // ����� REFER�NCIA DO �LBUM �����
    public GameObject painelAlbum;

    // ����� BOT�O CHAMANDO ESSA FUN��O �����
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
