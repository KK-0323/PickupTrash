using Unity.VisualScripting;
using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float scrSpeed = 10f;

    [Header("削除するZ座標")]
    [SerializeField] private float destroyZ = -20f;

    [Header("生成するプレハブ")]
    [SerializeField] private GameObject roadPrefab;

    [Header("道路1枚のzの長さ")]
    [SerializeField] private float roadLength = 30f;

    // 道路を生成したかのフラグ
    private bool roadSpawned = false;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        // 手前移動
        transform.Translate(Vector3.back * scrSpeed * Time.deltaTime);

        if (!roadSpawned && transform.position.z <= destroyZ + roadLength)
        {
            SpawnNextRoad();
        }

        if(transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }

    void SpawnNextRoad()
    {
        roadSpawned = true;
        Debug.Log("生成したよ");

        // 今の道路から道路の長さ分奥の座標を計算
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + (roadLength * 2));
    }
}
