using CodeBase.Scene;
using CodeBase.UI;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Lobby
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [Header("UI")]
        [SerializeField] private InputField _roomNameInputField;
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _joinRoomButton;
        [SerializeField] private Popup _errorPopup;

        public override void OnEnable()
        {
            base.OnEnable();

            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinToRoom);
        }

        public override void OnDisable()
        {
            base.OnDisable();

            _createRoomButton.onClick.RemoveListener(CreateRoom);
            _joinRoomButton.onClick.RemoveListener(JoinToRoom);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(ScenesName.Game);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            _errorPopup.Show(message);
        }

        private void CreateRoom()
        {
            PhotonNetwork.CreateRoom(_roomNameInputField.text);
        }

        private void JoinToRoom()
        {
            PhotonNetwork.JoinRoom(_roomNameInputField.text);
        }
    }
}