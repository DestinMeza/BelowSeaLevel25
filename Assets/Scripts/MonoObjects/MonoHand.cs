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

        public MonoCard[] UICards;

        [SerializeField]
        private PlayerHandConfig m_HandConfig;

        [SerializeField]
        private DeckConfig m_AvalibleCardDeck;

        private Deck m_ReferencedDeck;

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
            UpdateActiveHand();
        }

        public void OnSelectCallback(MonoCard monoCard)
        {
            Debug.Log($"Selecting: {monoCard.CardRef.GetType().Name}");
        }

        public void OnActivateCallback(MonoCard monoCard)
        {
            Debug.Log($"Activating: {monoCard.CardRef.GetType().Name}");

            CardManager.SetActiveCard(monoCard);
        }

        public void OnDrawCallback(MonoCard monoCard)
        {
            Debug.Log($"Drawing: {monoCard.CardRef.GetType().Name}");
        }

        private MonoCard GetInactiveCard()
        {
            return UICards.Where(x => !x.HasCard).FirstOrDefault();
        }

        public void Draw()
        {
            Debug.Log("Drawing Card...");
            Card pendingCard = m_ReferencedDeck.Draw();

            MonoCard inactiveCard = GetInactiveCard();
            inactiveCard.OnDraw(pendingCard);

            bool forceDiscard = ActiveHand.Length >= m_HandConfig.MaxCards + 1;

            List<MonoCard> monoCards = UICards.ToList();

            if (forceDiscard)
            {
                MonoCard monoCardToDiscard = null;

                for (int i = 0; i < monoCards.Count; i++)
                {
                    if (monoCards[i].HasCard)
                    {
                        monoCardToDiscard = monoCards[i];
                        break;
                    }
                }

                Discard(monoCardToDiscard);
            }

            UpdateActiveHand();
        }

        public void Discard(MonoCard cardToDiscard)
        {
            Debug.Log("Discarding Card...");
            GameManager.Player.AddScore(-10);

            cardToDiscard.OnDiscard();
            
            UpdateActiveHand();
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