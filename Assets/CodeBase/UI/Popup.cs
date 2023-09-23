using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Popup : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private CanvasGroup _canvasGroup;

        protected virtual void OnAwake() { }

        protected void ChangeVisibility(bool visible)
        {
            _canvasGroup.alpha = visible ? 1f : 0f;
            _canvasGroup.interactable = visible;
            _canvasGroup.blocksRaycasts = visible;
        }

        protected void Close()
        {
            ChangeVisibility(false);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            OnAwake();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
        }
    }
}