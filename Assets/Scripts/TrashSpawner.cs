using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Build.Content;

public class TrashSpwner : MonoBehaviour
{
    [Header("生成するゴミプレハブ")]
    [SerializeField] private GameObject[] trashPrefabs;

    [Header("再生成までの時間(秒)")]
    [SerializeField] private float respawnDelay = 10.0f;

    private GameObject currentTrash;
    private bool isWaitingForRespawn = false;

    void Start()
    {
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
        if (trashPrefabs == null || trashPrefabs.Length == 0) return;

        // ランダム生成
        int randomIndex = Random.Range(0, trashPrefabs.Length);
        currentTrash = Instantiate(trashPrefabs[randomIndex], transform.position, transform.rotation);

        // 親をスポーンポイントに
        currentTrash.transform.SetParent(transform);
    }

    private IEnumerator RespawnRoutine()
    {
        isWaitingForRespawn = true;

        // n秒待機
        yield return new WaitForSeconds(respawnDelay);

        // ゲーム進行中のみ生成する
        if (GameManager.Instance == null || !GameManager.Instance.IsGameOver)
        {
            SpawnTrash();
        }

        isWaitingForRespawn = false;
    }
}
