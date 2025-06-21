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
        private int drawIndex;
        
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

            for (int i = 0; i < Cards.Length; i++)
            {
                int randomSwapIndex = random.Next(0, Cards.Length);

                Card temp = Cards[randomSwapIndex];
                Cards[randomSwapIndex] = Cards[i];
                Cards[i] = temp;
            }
        }

        /// <summary>
        /// This draws a card from the deck from bottom of the deck to the top of the deck
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            if (Cards.Length == drawIndex + 1)
            {
                drawIndex = -1;
                Shuffle();
            }

            drawIndex++;

            return Cards[drawIndex];
        }
    }
}