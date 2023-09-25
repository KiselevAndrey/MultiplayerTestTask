using CodeBase.Settings;
using CodeBase.Utility.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Lobby
{
    [System.Serializable]
    public class NameAreaUI
    {
        [SerializeField] private InputField _nameInputField;
        [SerializeField] private Button _changeNameButton;

        public bool IsNameSaved => PlayerPrefsManager.GetName().HasContent();

        public void Awake() => 
            _nameInputField.text = PlayerPrefsManager.GetName();

        public void OnEnable() => 
            _changeNameButton.onClick.AddListener(ChangeName);

        public void OnDisable() => 
            _changeNameButton.onClick.RemoveListener(ChangeName);

        private void ChangeName() => 
            PlayerPrefsManager.SetName(_nameInputField.text);
    }
}