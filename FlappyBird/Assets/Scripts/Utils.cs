using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public static class Utils
    {
        public static bool IsWithinBounds(Vector2 pos)
        {
            float hori = 7f;
            float vert = 5f;
            return pos.x < hori && pos.x > -hori && pos.y < vert && pos.y > -vert;
        }
    }
}