using System;
using UnityEngine;

namespace CodeBase.Game.Player
{
    [Serializable]
    public class PlayerStats : IMoveStats, IDamageable
    {
        [field: SerializeField, Range(1f, 10f)] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField, Range(0, 1000)] public int RotationSpeed { get; private set; } = 720;

        [SerializeField] private PlayerHealth _health;

        public event Action OnDie;

        public void Init(PlayerRpcController rpcController) => 
            _health.Init(Die, rpcController);

        public void TakeDamage(int damage) =>
            _health.TakeDamage(damage);

        public void ChangeHealthVisual(int newHealthPercent) =>
            _health.ChangeHealthVisual(newHealthPercent);

        private void Die() =>
            OnDie?.Invoke();
    }
}