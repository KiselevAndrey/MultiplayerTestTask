using CodeBase.Game.Player;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Game.Coin
{
    public class CoinBehaviour : MonoBehaviour
    {
        private PhotonView _view;

        private void Awake() => 
            _view = GetComponent<PhotonView>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out PlayerBehaviour player))
            {
                player.PickupCoin();
                Invoke(nameof(Destroy), 0.1f);
            }
        }

        private void Destroy()
        {
            if(_view.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}