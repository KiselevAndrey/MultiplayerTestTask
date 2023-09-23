using CodeBase.Scene;
using Photon.Pun;
using UnityEngine.SceneManagement;

namespace CodeBase.Photon
{
    public class ConnecToPhotonServer : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene(ScenesName.Lobby);
        }
    }
}