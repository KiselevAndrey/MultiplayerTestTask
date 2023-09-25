using CodeBase.Utility;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Game.Coin
{
    public class CoinSpawner : MonoBehaviour
    {
        [Header("Spawn Parameters")]
        [SerializeField] private CoinBehaviour _coinPrefab;
        [SerializeField] private Transform _leftDownPoint;
        [SerializeField] private Transform _rightUpPoint;
        [SerializeField, Range(2f, 10f)] private float _spawnWaitTime = 5f;

        private bool _canSpawn;
        private WaitForSeconds _waitTime;

        private void Awake() => 
            _waitTime = new(_spawnWaitTime);

        private void OnEnable()
        {
            _canSpawn = true;
            StartCoroutine(Spawning());
        }

        private void OnDisable() => 
            _canSpawn = false;

        private IEnumerator Spawning()
        {
            while (_canSpawn)
            {
                Spawn();
                yield return _waitTime;
            }
        }

        private void Spawn()
        {
            var spawnPosition = RandomPosition.InRectangle(_leftDownPoint.position, _rightUpPoint.position);
            PhotonNetwork.Instantiate(_coinPrefab.name, spawnPosition, Quaternion.identity);
        }
    }
}