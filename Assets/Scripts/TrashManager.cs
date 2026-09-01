using UnityEngine;

public class TrashManager : MonoBehaviour
{
    [Header("ゴミ共通設定")]
    [SerializeField] private GameObject[] trashPrefabs;
    [SerializeField] private float respawnDelay = 5.0f;

    // プロパティ
    public GameObject[] TrashPrefabs => trashPrefabs;
    public float RespawnDelay => respawnDelay;
}
