using CodeBase.Utility;
using Photon.Pun;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Game.Player
{
    [System.Serializable]
    public class PlayerAttacker
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField, Range(0.1f, 2f)] private float _reloadTime = 1f;

        private PhotonView _view;
        private WaitForSeconds _reloadTimer;
        private Coroutine _shootCoroutine;
        private ICoroutineRunner _coroutineRunner;

        private bool _canShoot;

        public void Init(PhotonView view, ICoroutineRunner coroutineRunner)
        {
            _view = view;
            _coroutineRunner = coroutineRunner;
            _reloadTimer = new(_reloadTime);
        }

        public void ActiveGame(bool isActiveGame)
        {
            if(isActiveGame)
                StartShooting();
            else
                StopShooting();
        }

        public void StartShooting()
        {
            if (_canShoot == false)
            {
                _canShoot = true;
                _shootCoroutine = _coroutineRunner.StartCoroutine(Shooting());
            }
        }

        public void StopShooting()
        {
            _canShoot = false;
            if(_shootCoroutine != null)
                _coroutineRunner.StopCoroutine(_shootCoroutine);
        }

        private IEnumerator Shooting()
        {
            while(_canShoot)
            {
                yield return _reloadTimer;
                Shoot();
            }
        }

        private void Shoot()
        {
            var projectile = PhotonNetwork.Instantiate(_projectilePrefab.name, _projectileSpawnPoint.position, _projectileSpawnPoint.rotation)
                .GetComponent<Projectile>();
            projectile.Set(_view);
        }
    }
}