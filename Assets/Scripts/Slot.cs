using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _cells;

    public SpriteRenderer[] Cells => _cells;
}
