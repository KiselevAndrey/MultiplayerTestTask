
using System;
using UnityEngine;

namespace CodeBase.Game.Player
{
    [Serializable]
    public class PlayerWallet
    {
        [SerializeField, Range(1, 20)] private int _maxCoinsCount = 10;
        [SerializeField] private Bar _coinBar;

        private PlayerRpcController _rpcController;

        private int _coinsCount;

        public event Action WalletIsFull;

        public void Init(PlayerRpcController rpcController)
        {
            _rpcController = rpcController;
        }

        public void PickupCoin()
        {
            _coinsCount++;
            _rpcController.ChangeWalletVisual((byte)((_coinsCount * 100) / _maxCoinsCount));

            if(_coinsCount>= _maxCoinsCount) 
                WalletIsFull?.Invoke();
        }

        public void ChangeVisual(byte fullnessPercent)
        {
            _coinBar.SetValue(fullnessPercent * 0.01f);
        }
    }
}