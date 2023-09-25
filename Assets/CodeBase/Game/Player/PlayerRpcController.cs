using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game.Player
{
    public class PlayerRpcController : MonoBehaviour
    {
        private PlayerBehaviour _player;

        public void Init(PlayerBehaviour player) => 
            _player = player;

        public void TakeDamage(int damage) => 
            _player.View.RPC(nameof(TakeDamageRPC), RpcTarget.All, damage);

        public void ChangeHealthVisual(int newHealthPercent) => 
            _player.View.RPC(nameof(ChangeHealthVisualRPC), RpcTarget.All, newHealthPercent);

        private void OnEnable() => 
            _player.Stats.OnDie += OnPlayerDie;

        private void OnDisable() => 
            _player.Stats.OnDie -= OnPlayerDie;

        private void OnPlayerDie()
        {
            Debug.Log($"OnPlayerDie");
            PhotonNetwork.Destroy(gameObject);
        }

        [PunRPC]
        private void TakeDamageRPC(int damage)
        {
            Debug.Log($"TakeDamageRPC({damage}), _view.IsMine: {_player.View.IsMine}");
            if (_player.View.IsMine)
                _player.Stats.TakeDamage(damage);
        }

        [PunRPC]
        private void ChangeHealthVisualRPC(int newHealthPercent) => 
            _player.Stats.ChangeHealthVisual(newHealthPercent);
    }
}