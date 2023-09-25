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

        private void Awake()
        {
            View = GetComponent<PhotonView>();
            _rpcController = GetComponent<PlayerRpcController>();
            _rpcController.Init(this);
            Stats.Init(_rpcController);
            Attacker.Init(View, this);
        }

        private void OnDisable() => 
            Attacker.StopShooting();
    }
}