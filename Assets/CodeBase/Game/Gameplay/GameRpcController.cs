using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game.Gameplay
{
    public class GameRpcController : MonoBehaviour
    {
        private PhotonView _view;
        private GameController _controller;

        public void Init(PhotonView view, GameController gameController)
        {
            _view = view;
            _controller = gameController;
        }

        public void PlayerDie(int playerId) => 
            _view.RPC(nameof(PlayerDieRPC), RpcTarget.All, playerId);

        public void AddCreatedPlayer(global::Photon.Realtime.Player newPlayer, int playerId) =>
            _view.RPC(nameof(AddCreatedPlayerRPC), newPlayer, playerId);

        public void SomeoneFilledWallet() => 
            _view.RPC(nameof(SomeoneFilledWalletRPC), RpcTarget.All);


        [PunRPC]
        private void PlayerDieRPC(int playerId) => 
            _controller.SurviveController.PlayerDie(playerId);

        [PunRPC]
        private void AddCreatedPlayerRPC(int playerID) => 
            _controller.SurviveController.AddMe(playerID);


        [PunRPC]
        private void SomeoneFilledWalletRPC() =>
            _controller.CoinsWinController.SomeoneFilledWallet();
    }
}