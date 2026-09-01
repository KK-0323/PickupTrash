using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [Header("ゴミの獲得ポイント")]
    [SerializeField] private int scoreValue = 10;
    private bool isPlayerInRange = false;
    public int scoreVal => scoreValue;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectTrash();
        }
    }

    public void SetPlayerInRange(bool inRange)
    {
        isPlayerInRange = inRange;
    }

    private void CollectTrash()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        Destroy(gameObject);
    }
}
