using System;

namespace CodeBase.Game.Player
{
    public interface IDamageable
    {
        public event Action OnDie;

        public void TakeDamage(int damage);
    }
}