using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trash"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject trashObject)
    {
        Debug.Log("ゴミを取得しました: " + trashObject.name);

        Destroy(trashObject);
    }
}
