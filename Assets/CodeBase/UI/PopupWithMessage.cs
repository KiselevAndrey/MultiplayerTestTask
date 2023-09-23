using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PopupWithMessage : Popup
    {
        [SerializeField] private Text _messageText;


        public void Show(string message)
        {
            _messageText.text = message;

            ChangeVisibility(true);
        }

        protected override void OnAwake()
        {
            Close();
        }
    }
}