using UnityEngine;

namespace CodeBase.Utility
{
    public static class RandomPosition
    {
        public static Vector2 InRectangle(Vector2 leftDownPoint, Vector2 rightUpPoint)
        {
            return new Vector2(
                Random.Range(leftDownPoint.x, rightUpPoint.x),
                Random.Range(leftDownPoint.y, rightUpPoint.y));
        }
    }
}