using UnityEngine;
using System.Collections;

public class TrashSpwner : MonoBehaviour
{
    private GameObject[] trashPrefabs;
    public float respawnDelay = 5.0f;

    private GameObject currentTrash;
    private bool isWaitingForRespawn = false;

    void Start()
    {
        TrashManager manager = GetComponentInParent<TrashManager>();
        if (manager != null)
        {
            trashPrefabs = manager.TrashPrefabs;
            respawnDelay = manager.RespawnDelay;
        }
        else
        {
            Debug.Log("TrashManagerが見つかりません");
        }

        SpawnTrash();
    }

    void Update()
    {
        if (currentTrash == null && !isWaitingForRespawn)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private void SpawnTrash()
    {
        if (trashPrefabs == null || trashPrefabs.Length == 0)
        {
            return;
        }

        // ランダム生成
        int randomIndex = Random.Range(0, trashPrefabs.Length);
        currentTrash = Instantiate(trashPrefabs[randomIndex], transform.position, transform.rotation);
        currentTrash.transform.SetParent(transform);
    }

    private IEnumerator RespawnRoutine()
    {
        isWaitingForRespawn = true;

        yield return new WaitForSeconds(respawnDelay);

        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
        {
            isWaitingForRespawn = false;
            yield break;
        }

        SpawnTrash();

        isWaitingForRespawn = false;
    }
}
