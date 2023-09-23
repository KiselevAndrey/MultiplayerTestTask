using CodeBase.Scene;
using CodeBase.UI;
using CodeBase.Utility.Extension;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Lobby
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        [SerializeField] private RoomAreaUI _roomArea;
        [SerializeField] private NameAreaUI _nameArea;
        [Space]
        [SerializeField] private PopupWithMessage _errorPopup;

        public override void OnEnable()
        {
            base.OnEnable();

            _roomArea.OnEnable();
            _nameArea.OnEnable();
        }

        public override void OnDisable()
        {
            base.OnDisable();

            _roomArea.OnDisable();
            _nameArea.OnDisable();
        }

        public override void OnJoinedRoom() =>
            PhotonNetwork.LoadLevel(ScenesName.Game);

        public override void OnJoinRoomFailed(short returnCode, string message) =>
            _errorPopup.Show(message);

        private void Awake()
        {
            _roomArea.Init(CreateRoom, JoinToRoom);
            _nameArea.Awake();
        }

        private void CreateRoom(string roomName)
        {
            if (InputsHasContent(roomName))
                PhotonNetwork.CreateRoom(roomName);
        }

        private void JoinToRoom(string roomName)
        {
            if (InputsHasContent(roomName))
                PhotonNetwork.JoinRoom(roomName);
        }

        private bool InputsHasContent(string roomName)
        {
            if(roomName.HasContent() == false)
            {
                _errorPopup.Show("Input field \"Room name\" is empty");
                return false;
            }
            else if(_nameArea.IsNameSaved == false)
            {
                _errorPopup.Show("Input field \"Name\" is empty or not saved");
                return false;
            }

            return true;
        }
    }
}