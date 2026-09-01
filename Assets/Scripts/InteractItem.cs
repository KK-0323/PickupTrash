using UnityEngine;

public class InteractItem : MonoBehaviour
{
    private TrashItem parentTrash;

    void Start()
    {
        parentTrash = GetComponent<TrashItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentTrash?.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            parentTrash?.SetPlayerInRange(false);
        }
    }
}
