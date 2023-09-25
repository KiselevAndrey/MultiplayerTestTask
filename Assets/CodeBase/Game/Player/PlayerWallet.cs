
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

        public event Action WalletIsFull;

        public int CoinsCount { get; private set; }

        public void Init(PlayerRpcController rpcController) => 
            _rpcController = rpcController;

        public void PickupCoin()
        {
            CoinsCount++;
            _rpcController.ChangeWalletVisual((byte)((CoinsCount * 100) / _maxCoinsCount));

            if(CoinsCount >= _maxCoinsCount) 
                WalletIsFull?.Invoke();
        }

        public void ChangeVisual(byte fullnessPercent) => 
            _coinBar.SetValue(fullnessPercent * 0.01f);
    }
}