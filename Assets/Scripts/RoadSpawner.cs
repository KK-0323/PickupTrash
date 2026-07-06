using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [Header("自動生成するプレハブ")]
    [SerializeField] private GameObject roadPrefab;

    [Header("道路を生成するZ座標")]
    [SerializeField] private float spawnTargetZ = 40f;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void SpawnNextRoad()
    {
        Vector3 spawnPos = new Vector3(0f, 0f, spawnTargetZ);
        Instantiate(roadPrefab, spawnPos, Quaternion.identity);
    }
}
