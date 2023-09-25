using UnityEngine;

namespace CodeBase.Game
{
    [System.Serializable]
    public class Bar : MonoBehaviour
    {
        [SerializeField] private Transform _bar;

        public void SetValue(float value) => 
            _bar.transform.localScale = new(value, 1, 1);
    }
}