using Photon.Pun;
using System;
using UnityEngine;

namespace CodeBase.Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField, Range(5f, 20f)] private float _speed = 10f;

        private Rigidbody2D _rigidbody;
        private PhotonView _shooterView;

        private readonly int _damage = 1;

        public void Set(PhotonView view) => 
            _shooterView = view;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = transform.up * _speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_shooterView == null || _shooterView.IsMine == false)
                return;

            if (collision.TryGetComponent(out IHitReactor hitReactor))
                hitReactor.TakeDamage(_damage);

            Destroy();
        }

        private void Destroy() =>
            PhotonNetwork.Destroy(gameObject);
    }
}