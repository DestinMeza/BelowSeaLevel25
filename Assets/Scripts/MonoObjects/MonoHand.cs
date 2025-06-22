using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

namespace BelowSeaLevel_25
{
    public class MonoHand : MonoBehaviour, IUIElement
    {
        public MonoCard[] ActiveHand => GetActiveHand();
        public MonoCard LastCard => GetLastCard();
        public MonoCard StartingCard => GetFirstCard();
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
                UICards[i].OnSelectCallback += OnSelectCallback;
                UICards[i].OnActivateCallback += OnActivateCallback;
                UICards[i].OnDrawCallback += OnDrawCallback;
            }

            m_ReferencedDeck = new Deck(m_AvalibleCardDeck.Cards);
            m_SelectedIndex = UICards.Length - 1;
            UpdateActiveHand();
        }

        public void OnSelectCallback(Card card)
        {
            Debug.Log($"Selecting: {card.GetType().Name}");
        }

        public void OnActivateCallback(Card card)
        {
            Debug.Log($"Activating: {card.GetType().Name}");

            CardManager.SetActiveCard(card);
        }

        public void OnDrawCallback(Card card)
        {
            Debug.Log($"Drawing: {card.GetType().Name}");
        }

        private MonoCard GetInactiveCard()
        {
            return UICards.Where(x => !x.isActiveAndEnabled).FirstOrDefault();
        }

        public void Draw()
        {
            Debug.Log("Drawing Card...");
            Card pendingCard = m_ReferencedDeck.Draw();

            MonoCard inactiveCard = GetInactiveCard();
            inactiveCard.OnDraw(pendingCard);

            bool forceDiscard = ActiveHand.Length >= m_HandConfig.MaxCards + 1;

            List<MonoCard> monoCards = UICards.ToList();
            monoCards.Sort(SortByActiveCard);

            if (forceDiscard)
            {
                for (int i = 0; i < monoCards.Count; i++)
                {
                    Debug.Log($"Card {i}: {monoCards[i].CardRef}");
                }

                Discard(monoCards.First());
            }

            UpdateActiveHand();
        }

        private int SortByActiveCard(MonoCard card, MonoCard other)
        {
            if (card.HasCard && !other.HasCard)
            {
                return -1;
            }
            if (!card.HasCard && other.HasCard)
            {
                return 1;
            }

            return 0;
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

        private MonoCard GetLastCard()
        {
            MonoCard[] activeCards = GetActiveHand();

            for (int i = activeCards.Length - 1; i >= 0; i--)
            {
                if (activeCards[i].HasCard)
                {
                    return activeCards[i];
                }
            }

            return null;
        }

        private MonoCard GetFirstCard()
        {
            MonoCard[] activeCards = GetActiveHand();

            for (int i = 0; i < 0; i++)
            {
                if (activeCards[i].HasCard)
                {
                    return activeCards[i];
                }
            }

            return null;
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}