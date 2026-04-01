using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public static class Utils
    {
        public static bool IsWithinBounds(Vector2 pos)
        {
            float hori = 8f;
            float vert = 6f;
            return pos.x < hori && pos.x > -hori && pos.y < vert && pos.y > -vert;
        }
    }

    public static class GameConstants
    {
        public const float ppu = 32f;
        public const float upp = 1f / ppu;
    }
}