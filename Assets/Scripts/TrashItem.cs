using System.Collections.Generic;
using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [Header("ゴミの設定")]
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private int playerCount = 1; // ゴミの回収に必要な人数

    public int scoreVal => scoreValue;

    private HashSet<GameObject> playerInRange = new HashSet<GameObject>();

    public void SetPlayerInRange(GameObject player, bool inRange)
    {
        if (player == null)
        {
            return;
        }

        if (inRange)
        {
            playerInRange.Add(player);
        }
        else
        {
            playerInRange.Remove(player);
        }
    }

    public bool TryCollect()
    {
        // プレイヤーの数を判定
        if (playerInRange.Count < playerCount)
        {
            Debug.Log($"[大型ゴミ] 回収にはあと {playerCount - playerInRange.Count} 人必要です");
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
