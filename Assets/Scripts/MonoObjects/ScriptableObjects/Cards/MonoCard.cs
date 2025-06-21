using Unity.VisualScripting;
using UnityEngine;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This card is the main object card
    /// </summary>
    public class MonoCard : MonoBehaviour
    {
        public delegate void SelectEvent(Card cardable);
        public delegate void ActivateEvent(Card cardable);
        public delegate void DrawEvent(Card cardable);


        public int HandIndex { get; private set; }
        public Card CardRef { get; private set; }
        public bool HasCard => null != CardRef;

        public SelectEvent OnSelectCallback = delegate { };
        public ActivateEvent OnActivateCallback = delegate { };
        public DrawEvent OnDrawCallback = delegate { };

        public void SetIndex(int handIndex)
        {
            HandIndex = handIndex;
        }

        public void OnDraw(Card card)
        {
            CardRef = card;
            CardRef.SetGameCard(this);
            OnDrawCallback(CardRef);
        }

        public void OnSelect()
        {
            OnSelectCallback(CardRef);
        }

        public void OnActivate()
        {
            OnActivateCallback(CardRef);
            OnDiscard();
        }

        public void OnDiscard()
        {
            CardRef.SetGameCard(null);
            CardRef = null;
        }

        #region Helper Functions

        public void Swap(MonoCard monoCard)
        {
            Card temp = monoCard.CardRef;
            monoCard.SetCard(CardRef);
            CardRef = temp;
        }

        protected void SetCard(Card card)
        {
            CardRef = card;
        }

        #endregion
    }
}
