using System;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// Card deck holds Card objects
    /// </summary>
    public class Deck
    {
        public Card[] Cards => m_Cards;
        // This is the seed for shuffling decks. 
        // This really just makes the shuffle consistent by choice for now.
        public const int SHUFFLE_SEED = 10101;
        private Card[] m_Cards;
        private int drawIndex = 0;
        
        public Deck(Card[] cards)
        {
            m_Cards = cards;
        }

        /// <summary>
        /// Shuffle is called to change the order of all the cards in the Deck
        /// </summary>
        public void Shuffle()
        {
            Random random = new Random(SHUFFLE_SEED);

            for (int i = 0; i < m_Cards.Length; i++)
            {
                int randomSwapIndex = random.Next(0, m_Cards.Length);

                Card temp = m_Cards[randomSwapIndex];
                m_Cards[randomSwapIndex] = m_Cards[i];
                m_Cards[i] = temp;
            }
        }

        /// <summary>
        /// This draws a card from the deck from bottom of the deck to the top of the deck
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card cardToReturn = m_Cards[drawIndex];

            if (m_Cards.Length == drawIndex + 1)
            {
                drawIndex = -1;
                Shuffle();
            }

            drawIndex++;
            return cardToReturn;
        }
    }
}