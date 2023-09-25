using CodeBase.Game.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    [Serializable]
    public class SurviveController
    {
        [SerializeField] private PlayerSpawner _playerSpawner;

        private readonly List<int> _activePlayersIDs = new();

        private GameRpcController _rpcController;
        private Action<bool, bool> OnActivePlayersCountChangedAction;
        private Action<PlayerBehaviour> OnPlayerCreatedAction;

        private bool _localIsLive;

        private int PlayerId => Player.View.CreatorActorNr;

        public PlayerBehaviour Player { get; private set; }

        public void Init(GameRpcController rpcController, Action<bool, bool> onActivePlayersCountChanged, Action<PlayerBehaviour> onPlayerCreated)
        {
            _rpcController = rpcController;
            OnActivePlayersCountChangedAction = onActivePlayersCountChanged;
            OnPlayerCreatedAction = onPlayerCreated;
        }

        public void OnEnable() =>
            _playerSpawner.PlayerCreated += OnPlayerCreated;

        public void OnDisable() =>
            _playerSpawner.PlayerCreated -= OnPlayerCreated;

        public void OnPlayerEnteredRoom(global::Photon.Realtime.Player newPlayer)
        {
            _activePlayersIDs.Add(newPlayer.ActorNumber);
            _rpcController.AddCreatedPlayer(newPlayer, PlayerId);
            OnActivePlayersCountChanged();
        }

        public void OnPlayerLeftRoom(global::Photon.Realtime.Player otherPlayer)
        {
            if (otherPlayer.IsLocal == false
                && _activePlayersIDs.Remove(otherPlayer.ActorNumber))
                OnActivePlayersCountChanged();
        }

        public void DisablePlayer()
        {
            if (Player != null)
                Player.ActiveGame(false);
        }

        public void PlayerDie(int CreatorActorNr)
        {
            if (_activePlayersIDs.Remove(CreatorActorNr))
                OnActivePlayersCountChanged();
        }

        public void AddMe(int playerID)
        {
            _activePlayersIDs.Add(playerID);
            OnActivePlayersCountChanged();
        }

        private void OnPlayerCreated(GameObject player)
        {
            Player = player.GetComponent<PlayerBehaviour>();
            Player.Stats.OnDie += OnPlayerDie;
            _localIsLive = true;
            _activePlayersIDs.Add(Player.View.CreatorActorNr);
            OnPlayerCreatedAction(Player);

            OnActivePlayersCountChanged();
        }

        private void OnPlayerDie()
        {
            Player.Stats.OnDie -= OnPlayerDie;
            _localIsLive = false;
            _rpcController.PlayerDie(PlayerId);
        }

        private void OnActivePlayersCountChanged()
        {
            var isActiveGame = _activePlayersIDs.Count > 1;
            //isActiveGame = true;

            Player.ActiveGame(isActiveGame);

            OnActivePlayersCountChangedAction(isActiveGame, _localIsLive);
        }
    }
}