using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [Header("ゴミの獲得ポイント")]
    [SerializeField] private int scoreValue = 10;

    private bool isPlayerInRange = false;
    public int scoreVal => scoreValue;

    public void SetPlayerInRange(bool inRange)
    {
        isPlayerInRange = inRange;
    }

    public bool TryCollect()
    {
        if (!isPlayerInRange)
        {
            return false;
        }

        CollectTrash();
        return true;
    }

    private void CollectTrash()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.playCollectSE();
        }

        Destroy(gameObject);
    }
}
