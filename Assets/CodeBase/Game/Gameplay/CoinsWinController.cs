using CodeBase.Game.Coin;
using CodeBase.Game.Player;
using Photon.Pun;
using System;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    [Serializable]
    public class CoinsWinController
    {
        [SerializeField] private CoinSpawner _coinSpawner;

        private GameRpcController _rpcController;
        private PlayerBehaviour _player;
        private Action<bool, bool> OnLocalFilledWallet;

        private bool _localFilledWallet;

        public void Init(GameRpcController rpcController, Action<bool, bool> onLocalFilledWallet)
        {
            _rpcController = rpcController;
            OnLocalFilledWallet = onLocalFilledWallet;
        }

        public void ActiveSpawn(bool enable) => 
            _coinSpawner.enabled = enable;

        public void OnPlayerCreated(PlayerBehaviour player)
        {
            _player = player;
            _player.Wallet.WalletIsFull += OnWalletIsFull;
        }

        public void SomeoneFilledWallet() =>
            OnLocalFilledWallet(false, _localFilledWallet);

        private void OnWalletIsFull()
        {
            _player.Wallet.WalletIsFull -= OnWalletIsFull;
            _localFilledWallet = true;

            _rpcController.SomeoneFilledWallet();
        }
    }
}