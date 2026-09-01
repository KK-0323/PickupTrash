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
        int scoreToAdd = 0;

        TrashItem trashItem = trashObject.GetComponent<TrashItem>();
        if(trashItem != null)
        {
            scoreToAdd = trashItem.scoreVal;
        }
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreToAdd);
        }

        Debug.Log(trashObject.name + "を取得しました");
        Destroy(trashObject);
    }
}
