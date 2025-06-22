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

        public Card[] Cards => GetNewCardInstances();

        private Card[] GetNewCardInstances()
        {
            Card[] cards = new Card[m_Cards.Length];
            for (int i = 0; i < m_Cards.Length; i++)
            {
                cards[i] = Card.CreateInstance(m_Cards[i].GetType()) as Card;
                cards[i].Clone(m_Cards[i]);
            }

            return cards;
        }
    }
}