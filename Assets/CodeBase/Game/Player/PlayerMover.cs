using UnityEngine;

namespace CodeBase.Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        private FloatingJoystick _joystick;
        private Rigidbody2D _rigidbody;
        private PlayerModel _model;
        private IMoveStats _moveStats;

        public void Init(FloatingJoystick joystick, Quaternion spawnRotation)
        {
            _joystick = joystick;
            _moveStats = GetComponent<PlayerBehaviour>().Stats;
            _model = GetComponentInChildren<PlayerModel>();
            _model.transform.localRotation = spawnRotation;

            InitRigidBody();
        }

        private void Start()
        {
            if (_joystick == null)
                Destroy(this);
        }

        private void InitRigidBody()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.simulated = true;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _joystick.Direction * _moveStats.MoveSpeed;

            if(_joystick.Direction != Vector2.zero)
            {
                var toRotation = Quaternion.LookRotation(Vector3.forward, _joystick.Direction);
                _model.transform.rotation = Quaternion.RotateTowards(_model.transform.rotation, toRotation, _moveStats.RotationSpeed * Time.deltaTime);
            }
        }
    }
}