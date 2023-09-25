using CodeBase.Utility;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game.Player
{
    [RequireComponent(typeof(PhotonView))]
    public class PlayerBehaviour : MonoBehaviour, ICoroutineRunner
    {
        [field: SerializeField] public PlayerStats Stats { get; private set; }
        [field: SerializeField] public PlayerAttacker Attacker { get; private set; }
        [field: SerializeField] public PlayerWallet Wallet { get; private set; }

        public PhotonView View { get; private set; }

        private PlayerMover _mover;
        private PlayerRpcController _rpcController;

        public void ActiveGame(bool isActiveGame)
        {
            if (View.IsMine == false)
                return;

            if(_mover == null)
                _mover = GetComponent<PlayerMover>();
            _mover.enabled = isActiveGame;
            
            Attacker.ActiveGame(isActiveGame);
        }

        public void PickupCoin()
        {
            if (View.IsMine)
                Wallet.PickupCoin();
        }

        private void Awake()
        {
            View = GetComponent<PhotonView>();
            _rpcController = GetComponent<PlayerRpcController>();
            _rpcController.Init(this);
            Stats.Init(_rpcController);
            Wallet.Init(_rpcController);
            Attacker.Init(View, this);

            if (View.IsMine)
                GetComponentInChildren<PlayerModel>().ChangeColor(Color.cyan);
        }

        private void OnDisable() => 
            Attacker.StopShooting();
    }
}