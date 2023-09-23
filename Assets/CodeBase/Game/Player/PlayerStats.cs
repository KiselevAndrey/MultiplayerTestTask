using UnityEngine;

namespace CodeBase.Game.Player
{
    public class PlayerStats : MonoBehaviour, IMoveStats
    {
        [field: SerializeField, Range(1f, 10f)] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField, Range(0, 1000)] public int RotationSpeed { get; private set; } = 720;
    }
}