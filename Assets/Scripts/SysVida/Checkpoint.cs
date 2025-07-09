using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaManager vm = FindFirstObjectByType<VidaManager>();
            if (vm != null)
            {
                vm.DefinirCheckpoint(transform);
                Debug.Log("Checkpoint atualizado: " + name);
            }
        }
    }
}
