using UnityEngine;

public class DetectorDePlataforma : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SenoideM>() != null)
        {
            transform.root.SetParent(other.transform); // Player gruda na plataforma
            Debug.Log("Player colado à plataforma.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SenoideM>() != null)
        {
            transform.root.SetParent(null); // Player se solta
        }
    }
}
