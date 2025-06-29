using UnityEngine;
using UnityEngine.UI;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This card is the main object card
    /// </summary>
    public class MonoCard : MonoBehaviour, IUIElement
    {
        #region Events
        public delegate void SelectEvent(Card cardable);
        public delegate void ActivateEvent(Card cardable);
        public delegate void DrawEvent(Card cardable);

        public SelectEvent OnSelectCallback = delegate { };
        public ActivateEvent OnActivateCallback = delegate { };
        public DrawEvent OnDrawCallback = delegate { };

        #endregion

        public int HandIndex { get; private set; }
        public Card CardRef { get; private set; }
        public bool HasCard => null != CardRef;

        public Image CardImage;
        public TMPro.TextMeshProUGUI CardTextField;

        public void SetIndex(int handIndex)
        {
            HandIndex = handIndex;
        }

        public void OnDraw(Card card)
        {
            CardRef = card;

            CardImage.sprite = card.GetCardImage();
            CardTextField.text = card.GetCardDetails();

            CardRef.SetGameCard(this);
            OnDrawCallback(CardRef);
            SetActive(true);
        }

        public void OnSelect()
        {
            OnSelectCallback(CardRef);
        }

        public void OnActivate()
        {
            OnActivateCallback(CardRef);
        }

        public void OnDiscard()
        {
            CardRef.SetGameCard(null);
            CardRef = null;
            SetActive(false);
        }

        public void SetCard(Card card)
        {
            CardRef = card;
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
