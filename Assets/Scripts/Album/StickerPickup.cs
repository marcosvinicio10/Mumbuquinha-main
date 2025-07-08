using UnityEngine;

public class StickerPickup : MonoBehaviour
{
    public int id;
    public Sprite stickerImage;
    [TextArea(2, 4)]
    public string mensagemFigurinha = "Você encontrou uma nova figurinha!";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StickerManager manager = FindFirstObjectByType<StickerManager>();
            if (manager != null)
            {
                manager.ColetarFigurinha(id);
            }

            StickerDialogUI ui = FindFirstObjectByType<StickerDialogUI>();
            if (ui != null)
            {
                ui.MostrarFigurinha(stickerImage, mensagemFigurinha);
            }

            Destroy(gameObject);
        }
    }
}
