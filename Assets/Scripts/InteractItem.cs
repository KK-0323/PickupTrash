using UnityEngine;

public class InteractItem : MonoBehaviour
{
    private TrashItem parentTrash;

    void Start()
    {
        parentTrash = GetComponentInParent<TrashItem>();
        if(parentTrash == null )
        {
            Debug.LogError("TrashItemが見つかりません");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parentTrash?.SetPlayerInRange(other.gameObject, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            parentTrash?.SetPlayerInRange(other.gameObject, false);
        }
    }
}
