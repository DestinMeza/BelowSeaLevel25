using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace BelowSeaLevel_25
{
    public class MonoHand : MonoBehaviour
    {
        public MonoCard[] ActiveHand => GetActiveHand();
        public MonoCard LastCard => UICards.Last();
        public MonoCard StartingCard => UICards.First();
        public int SelectedIndex => m_SelectedIndex;

        public MonoCard[] UICards;

        [SerializeField]
        private PlayerHandConfig m_HandConfig;

        [SerializeField]
        private DeckConfig m_AvalibleCardDeck;

        private Deck m_ReferencedDeck;
        private int m_SelectedIndex;

        public void Init()
        {
            UICards = GetComponentsInChildren<MonoCard>();

            Debug.Log($"Found Cards: {UICards.Length}");

            for (int i = 0; i < UICards.Length; i++)
            {
                UICards[i].SetIndex(i);
            }

            m_ReferencedDeck = new Deck(m_AvalibleCardDeck.Cards);
            m_SelectedIndex = UICards.Length - 1;
            UpdateActiveHand();
        }

        private void PushDown()
        { 
            for (int i = 1; i < UICards.Length - 1; i++)
            {
                UICards[i + 1].Swap(UICards[i]);
            }
        }

        public void Draw()
        {
            Debug.Log("Drawing Card...");
            Card pendingCard = m_ReferencedDeck.Draw();

            bool forceDiscard = ActiveHand.Length == m_HandConfig.MaxCards;

            if (forceDiscard)
            {
                LastCard.OnDiscard();
            }

            PushDown();

            StartingCard.OnDraw(pendingCard);
            UpdateActiveHand();
        }

        public void Discard(MonoCard cardToDiscard)
        {
            Debug.Log("Discarding Card...");
            cardToDiscard.OnDiscard();
            UpdateActiveHand();
        }

        public void SetSelectedIndex(int index)
        {
            m_SelectedIndex = index;
        }

        public void UpdateActiveHand()
        {
            for (int i = 0; i < UICards.Length; i++)
            {
                UICards[i].gameObject.SetActive(false);
            }

            MonoCard[] activeHand = GetActiveHand();

            for (int i = 0; i < activeHand.Length; i++)
            {
                activeHand[i].gameObject.SetActive(true);
            }
        }

        private MonoCard[] GetActiveHand()
        {
            List<MonoCard> activeCards = new();

            for (int i = 0; i < UICards.Length; i++)
            {
                if (!UICards[i].HasCard)
                {
                    continue;
                }

                activeCards.Add(UICards[i]);
            }

            return activeCards.ToArray();
        }
    }
}