using UnityEngine;

namespace CodeBase.Camera
{
    public class CameraSizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _target;
        [SerializeField] private Orientation _orientation;

        private enum Orientation { Heigth, Width, Auto }

        private void Start()
        {
            switch (_orientation)
            {
                case Orientation.Heigth:
                    UnityEngine.Camera.main.orthographicSize = _target.bounds.size.y / 2;
                    break;
                case Orientation.Width:
                    UnityEngine.Camera.main.orthographicSize = _target.bounds.size.x * Screen.height / Screen.width * 0.5f;
                    break;
                case Orientation.Auto:
                    float screenRatio = (float)Screen.width / Screen.height;
                    float targetRatio = _target.bounds.size.x / _target.bounds.size.y;

                    float differenceInSize = (targetRatio < screenRatio) ? targetRatio / screenRatio : 1;
                    UnityEngine.Camera.main.orthographicSize = _target.bounds.size.y / 2 * differenceInSize;
                    break;
                default:
                    break;
            }
        }
    }
}