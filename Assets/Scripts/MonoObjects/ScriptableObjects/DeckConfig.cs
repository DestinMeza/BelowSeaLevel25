using UnityEngine;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This object holds a Deck configuration. This is more of a helper for easily manipulating
    /// the cards in the deck at design time.
    /// </summary>
    [CreateAssetMenu(fileName = "DeckConfig", menuName = "Scriptable Objects/DeckConfig")]
    public class DeckConfig : ScriptableObject
    {
        [SerializeField]
        private Card[] m_Cards;
        
        public Card[] Cards => m_Cards;
    }
}