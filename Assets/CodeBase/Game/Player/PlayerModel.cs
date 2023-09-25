using UnityEngine;

namespace CodeBase.Game.Player
{
    public class PlayerModel : MonoBehaviour
    {
        public void ChangeColor(Color color) =>
            GetComponent<SpriteRenderer>().color = color;
    }
}