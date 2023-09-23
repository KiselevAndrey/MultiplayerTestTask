using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Lobby
{
    [Serializable]
    public class RoomAreaUI
    {
        [SerializeField] private InputField _roomNameInputField;
        [SerializeField] private Button _createRoomButton;
        [SerializeField] private Button _joinRoomButton;

        private Action<string> _createRoom, _joinToRoom;

        public void Init(Action<string> createRoom, Action<string> joinToRoom)
        {
            _createRoom = createRoom;
            _joinToRoom = joinToRoom;
        }

        public void OnEnable()
        {
            _createRoomButton.onClick.AddListener(CreateRoom);
            _joinRoomButton.onClick.AddListener(JoinToRoom);
        }

        public void OnDisable()
        {
            _createRoomButton.onClick.RemoveListener(CreateRoom);
            _joinRoomButton.onClick.RemoveListener(JoinToRoom);
        }

        private void CreateRoom() =>
            _createRoom(_roomNameInputField.text);

        private void JoinToRoom() =>
            _joinToRoom(_roomNameInputField.text);
    }
}