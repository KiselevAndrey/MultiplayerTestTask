using CodeBase.Game.Player;
using CodeBase.Utility;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Spawn Parameters")]
        [SerializeField] private PlayerBehaviour _playerPrefab;
        [SerializeField] private Transform _leftDownPoint;
        [SerializeField] private Transform _rightUpPoint;

        [Header("Player Init Parameters")]
        [SerializeField] private FloatingJoystick _joystick;

        private void Start()
        {
            var spawnPosition = RandomPosition.InRectangle(_leftDownPoint.position, _rightUpPoint.position);
            var player = PhotonNetwork.Instantiate(_playerPrefab.name, spawnPosition, RandomRotation.In2D());
            InitPlayer(player);
        }

        private void InitPlayer(GameObject player)
        {
            var mover = player.AddComponent<PlayerMover>();
            mover.Init(_joystick);
        }
    }
}