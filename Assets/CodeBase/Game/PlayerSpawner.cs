using CodeBase.Game.Player;
using CodeBase.Utility;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _playerPrefab;
        [SerializeField] private Transform _leftDownPoint;
        [SerializeField] private Transform _rightUpPoint;

        private void Start()
        {
            var spawnPosition = RandomPosition.InRectangle(_leftDownPoint.position, _rightUpPoint.position);
            PhotonNetwork.Instantiate(_playerPrefab.name, spawnPosition, RandomRotation.In2D());
        }
    }
}