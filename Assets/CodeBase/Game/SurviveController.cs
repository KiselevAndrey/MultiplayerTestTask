using CodeBase.Game.Player;
using CodeBase.Scene;
using CodeBase.UI;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game
{
    [RequireComponent(typeof(PhotonView))]
    public class SurviveController : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerSpawner _spawner;
        [SerializeField] private PopupWithMessage _finalizePopup;

        private PhotonView _view;
        private PlayerBehaviour _player;
        private readonly List<int> _activePlayersIDs = new();

        private bool _localIsLive;
        private bool _gameStarted;

        public override void OnEnable()
        {
            base.OnEnable();
            _spawner.PlayerCreated += OnPlayerCreated;
            _finalizePopup.OnClose += OnCloseFinalizePopup;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            _spawner.PlayerCreated -= OnPlayerCreated;
            _finalizePopup.OnClose -= OnCloseFinalizePopup;
        }

        public override void OnPlayerEnteredRoom(global::Photon.Realtime.Player newPlayer)
        {
            _activePlayersIDs.Add(newPlayer.ActorNumber);
            _view.RPC(nameof(AddMeRPC), newPlayer, _player.View.CreatorActorNr);
            OnActivePlayersCountChanged();
        }

        public override void OnPlayerLeftRoom(global::Photon.Realtime.Player otherPlayer)
        {
            if (otherPlayer.IsLocal == false
                && _activePlayersIDs.Remove(otherPlayer.ActorNumber))
                OnActivePlayersCountChanged();
        }

        private void Awake() => 
            _view = GetComponent<PhotonView>();

        private void OnPlayerCreated(GameObject player)
        {
            _player = player.GetComponent<PlayerBehaviour>();
            _player.Stats.OnDie += OnPlayerDie;
            _localIsLive = true;
            _activePlayersIDs.Add(_player.View.CreatorActorNr);
            OnActivePlayersCountChanged();
        }

        private void OnPlayerDie()
        {
            _player.Stats.OnDie -= OnPlayerDie;
            _localIsLive = false;
            _view.RPC(nameof(PlayerDieRPC), RpcTarget.All, _player.View.CreatorActorNr);
            // показать что ты погиб
        }

        private void OnActivePlayersCountChanged()
        {
            Debug.Log(_activePlayersIDs.Count);

            var isActiveGame = _activePlayersIDs.Count > 1;
            _player.ActiveGame(isActiveGame);
            //_player.ActiveGame(true);

            if (isActiveGame)
                _gameStarted = true;
            else
            {
                if (_gameStarted)
                {
                    Debug.Log($"Выиграл локальный: {_localIsLive}");
                    ShowFinalMessage();
                }
            }
        }

        private void ShowFinalMessage()
        {
            var message = _localIsLive ?
                "You win!" :
                "You lose!";

            _finalizePopup.Show(message);
        }

        private void OnCloseFinalizePopup()
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel(ScenesName.Lobby);
        }

        [PunRPC]
        private void PlayerDieRPC(int CreatorActorNr)
        {
            if(_activePlayersIDs.Remove(CreatorActorNr))
                OnActivePlayersCountChanged();
        }

        [PunRPC]
        private void AddMeRPC(int playerID)
        {
            _activePlayersIDs.Add(playerID);
            OnActivePlayersCountChanged();
        }
    }
}