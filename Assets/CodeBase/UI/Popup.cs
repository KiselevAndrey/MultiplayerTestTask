using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Popup : MonoBehaviour
    {
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _closeButton;

        private CanvasGroup _canvasGroup;

        public void Show(string message)
        {
            _messageText.text = message;

            ChangeVisibility(true);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            Close();
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Close);
        }

        private void Close()
        {
            ChangeVisibility(false);
        }

        private void ChangeVisibility(bool visible)
        {
            _canvasGroup.alpha = visible ? 1f : 0f;
            _canvasGroup.interactable = visible;
            _canvasGroup.blocksRaycasts = visible;
        }
    }
}