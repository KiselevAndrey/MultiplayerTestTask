using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Popup : MonoBehaviour
    {
        [SerializeField] private bool _showWhenAwake;
        [SerializeField] private Button _closeButton;

        private CanvasGroup _canvasGroup;

        public event UnityAction OnClose;

        protected virtual void OnAwake() { }

        protected void ChangeVisibility(bool visible)
        {
            _canvasGroup.alpha = visible ? 1f : 0f;
            _canvasGroup.interactable = visible;
            _canvasGroup.blocksRaycasts = visible;
        }

        protected void OnCloseButtonClick()
        {
            ChangeVisibility(false);
            OnClose?.Invoke();
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            ChangeVisibility(_showWhenAwake);

            OnAwake();
        }

        private void OnEnable() => 
            _closeButton.onClick.AddListener(OnCloseButtonClick);

        private void OnDisable() => 
            _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }
}