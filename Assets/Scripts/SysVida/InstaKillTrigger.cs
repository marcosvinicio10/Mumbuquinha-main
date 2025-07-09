using UnityEngine;

public class InstaKillTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaManager vm = FindFirstObjectByType<VidaManager>();
            if (vm != null)
            {
                vm.MorteInstantanea();
            }
        }
    }
}
