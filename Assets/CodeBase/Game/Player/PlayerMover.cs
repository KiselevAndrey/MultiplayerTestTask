using UnityEngine;

namespace CodeBase.Game.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        private FloatingJoystick _joystick;
        private Rigidbody2D _rigidbody;
        private IMoveStats _moveStats;

        public void Init(FloatingJoystick joystick)
        {
            Debug.Log("Init");
            _joystick = joystick;
            _rigidbody = GetComponent<Rigidbody2D>();
            _moveStats = GetComponent<IMoveStats>();
        }

        private void Start()
        {
            Debug.Log("Start");
            if (_joystick == null)
                Destroy(this);
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _joystick.Direction * _moveStats.MoveSpeed;

            if(_joystick.Direction != Vector2.zero)
            {
                var toRotation = Quaternion.LookRotation(Vector3.forward, _joystick.Direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _moveStats.RotationSpeed * Time.deltaTime);
            }
        }
    }
}