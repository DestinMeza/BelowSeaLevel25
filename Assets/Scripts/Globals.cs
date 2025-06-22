using UnityEngine;

namespace BelowSeaLevel_25
{
    public static class Globals
    {
        public static class GameState
        {
            public static bool IsPlaying;
            public static MonoCannonEntity ActivePlayer;
            public static Vector3 PlayerStartingPosition;
        }

        public static class Enums
        {
            public enum UIState
            {
                FreePointer,
                HandMode,
                PlayCardMode,
                SelectDiscard
            }
        }
    }
}