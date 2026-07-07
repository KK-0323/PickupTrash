using Unity.VisualScripting;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [Header("道路のプレハブ")]
    [SerializeField] private GameObject roadPrefab;

    [Header("最初に生成する枚数")]
    [SerializeField] private int initialRoadCount = 5;

    // 道路の1つの長さ
    private float roadLength = 10.0f;

    // 次に道路を生成するZ座標
    private float nextSpawnZ = 0.0f;

    void Start()
    {
        for (int i = 0; i < initialRoadCount; i++)
        {
            SpawnRoad();
        }
    }

    public void SpawnRoad()
    {
        Vector3 spawnPos = new Vector3(0.0f, 0.0f, nextSpawnZ);

        GameObject newRoad = Instantiate(roadPrefab, spawnPos, Quaternion.identity);

        nextSpawnZ += roadLength;
    }
}
