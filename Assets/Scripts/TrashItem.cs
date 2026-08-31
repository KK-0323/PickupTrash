using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [Header("ゴミの獲得ポイント")]
    [SerializeField] private int scoreValue = 10;

    public int ScoreValue => scoreValue;
}
