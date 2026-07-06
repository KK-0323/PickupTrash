using Unity.VisualScripting;
using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float scrSpeed = 10f;

    [Header("削除するZ座標")]
    [SerializeField] private float destroyZ = -20f;

    private RoadSpawner roadSpawner;

    void Start()
    {
        Application.targetFrameRate = 60;
        roadSpawner = Object.FindAnyObjectByType<RoadSpawner>();
    }

    void Update()
    {
        // 手前移動
        transform.Translate(Vector3.back * scrSpeed * Time.deltaTime);

        if (transform.position.z < destroyZ)
        {
            if (roadSpawner != null)
            {
                roadSpawner.SpawnNextRoad();
            }
            Destroy(gameObject);
        }
    }
}