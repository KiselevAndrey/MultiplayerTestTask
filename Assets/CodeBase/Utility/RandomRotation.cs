using UnityEngine;

namespace CodeBase.Utility
{
    public static class RandomRotation
    {
        public static Quaternion In2D()
        {
            var z = Random.Range(0f, 360.0f);
            return Quaternion.Euler(0, 0, z);
        }
    }
}