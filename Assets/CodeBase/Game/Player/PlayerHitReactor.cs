using UnityEngine;

namespace CodeBase.Game.Player
{
    [RequireComponent(typeof(PlayerRpcController))]
    public class PlayerHitReactor : MonoBehaviour, IHitReactor
    {
        private PlayerRpcController _rpcController;

        public void TakeDamage(int damage) => 
            _rpcController.TakeDamage(damage);

        private void Awake() => 
            _rpcController = GetComponent<PlayerRpcController>();
    }
}