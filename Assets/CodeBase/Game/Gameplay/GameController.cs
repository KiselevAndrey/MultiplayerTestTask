using CodeBase.Game.Player;
using CodeBase.Scene;
using CodeBase.UI;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    [RequireComponent(typeof(PhotonView), typeof(GameRpcController))]
    public class GameController : MonoBehaviourPunCallbacks
    {
        [field: SerializeField] public SurviveController SurviveController { get; private set; }
        [field: SerializeField] public CoinsWinController CoinsWinController { get; private set; }

        [Header("UI")]
        [SerializeField] private PopupWithMessage _finalizePopup;

        private GameRpcController _rpcController;

        private bool _gameStarted;

        public override void OnEnable()
        {
            base.OnEnable();

            SurviveController.OnEnable();
            _finalizePopup.OnClose += OnCloseFinalizePopup;
        }

        public override void OnDisable()
        {
            base.OnDisable();

            SurviveController.OnDisable();
            _finalizePopup.OnClose -= OnCloseFinalizePopup;
        }

        public override void OnPlayerEnteredRoom(global::Photon.Realtime.Player newPlayer) => 
            SurviveController.OnPlayerEnteredRoom(newPlayer);

        public override void OnPlayerLeftRoom(global::Photon.Realtime.Player otherPlayer) => 
            SurviveController.OnPlayerLeftRoom(otherPlayer);

        private void Awake()
        {
            var view = GetComponent<PhotonView>();
            _rpcController = GetComponent<GameRpcController>();
            _rpcController.Init(view, this);
            SurviveController.Init(_rpcController, OnGameActiveChanged, OnPlayerCreated);
            CoinsWinController.Init(_rpcController, OnGameActiveChanged);
        }

        private void OnGameActiveChanged(bool isActiveGame, bool localIsWin)
        {
            if (isActiveGame)
            {
                _gameStarted = true;
                CoinsWinController.ActiveSpawn(true);
            }
            else if (_gameStarted)
            {
                ShowFinalMessage(localIsWin);
                CoinsWinController.ActiveSpawn(false);
                SurviveController.DisablePlayer();
            }
        }

        private void ShowFinalMessage(bool youWin)
        {
            var message = youWin ?
                "You win!" :
                "You lose!";

            _finalizePopup.Show(message);
        }

        private void OnPlayerCreated(PlayerBehaviour player) => 
            CoinsWinController.OnPlayerCreated(player);

        private void OnCloseFinalizePopup()
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(ScenesName.Lobby);
        }
    }
}