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

        public void ChangeHealthVisual(byte newHealthPercent) => 
            _player.View.RPC(nameof(ChangeHealthVisualRPC), RpcTarget.All, newHealthPercent);

        public void ChangeWalletVisual(byte walletFullnessPercent) =>
            _player.View.RPC(nameof(ChangeWalletVisualRPC), RpcTarget.All, walletFullnessPercent);

        private void OnEnable() => 
            _player.Stats.OnDie += OnPlayerDie;

        private void OnDisable() => 
            _player.Stats.OnDie -= OnPlayerDie;

        private void OnPlayerDie() => 
            PhotonNetwork.Destroy(gameObject);

        [PunRPC]
        private void TakeDamageRPC(int damage)
        {
            if (_player.View.IsMine)
                _player.Stats.TakeDamage(damage);
        }

        [PunRPC]
        private void ChangeHealthVisualRPC(byte newHealthPercent) => 
            _player.Stats.ChangeHealthVisual(newHealthPercent);

        [PunRPC]
        private void ChangeWalletVisualRPC(byte walletFullnessPercent) =>
            _player.Wallet.ChangeVisual(walletFullnessPercent);
    }
}