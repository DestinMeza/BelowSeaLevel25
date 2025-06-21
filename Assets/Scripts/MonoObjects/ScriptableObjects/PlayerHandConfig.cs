using UnityEngine;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This is the card hand data to hold player config of the cards
    /// the player can have.
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerHandConfig", menuName = "Scriptable Objects/PlayerHandConfig")]
    public class PlayerHandConfig : ScriptableObject
    {
        public int MaxCards;
        public int MinCards;
    }
}