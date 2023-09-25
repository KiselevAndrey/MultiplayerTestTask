using System;
using UnityEngine;

namespace CodeBase.Game.Player
{
    [Serializable]
    public class PlayerHealth
    {
        [SerializeField, Range(1, 20)] private int _maxHealth = 10;
        [SerializeField] private Bar _healthBar;

        private int _health;

        private Action Die;
        private PlayerRpcController _rpcController;

        public bool IsLive => _health > 0;

        public void Init(Action die, PlayerRpcController rpcController) 
        {
            Die = die;
            _rpcController = rpcController;
            _health = _maxHealth;
            //_rpcController.ChangeHealthVisual(100);     // 100% of full health
        }

        public void TakeDamage(int damage)
        {
            if (_health <= damage)
            {
                OnDie();
                return;
            }

            _health -= damage;
            _rpcController.ChangeHealthVisual((_health * 100) / _maxHealth);
        }

        public void ChangeHealthVisual(int newHealthPercent) => 
            _healthBar.SetValue(newHealthPercent * 0.01f);

        private void OnDie()
        {
            _health = 0;

            Die?.Invoke();
        }
    }
}