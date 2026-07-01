using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float scrSpeed = 10f;

    [Header("削除するZ座標")]
    [SerializeField] private float destroyZ = -20f;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        transform.Translate(Vector3.back * scrSpeed * Time.deltaTime);

        if(transform.position.z < destroyZ)
        {
            Destroy(gameObject);
        }
    }
}
