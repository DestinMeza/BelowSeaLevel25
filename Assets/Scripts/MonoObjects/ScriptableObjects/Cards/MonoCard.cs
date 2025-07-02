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
        public delegate void SelectEvent(MonoCard self);
        public delegate void ActivateEvent(MonoCard self);
        public delegate void DrawEvent(MonoCard self);

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
            OnDrawCallback(this);
        }

        public void OnSelect()
        {
            OnSelectCallback(this);
        }

        public void OnActivate()
        {
            OnActivateCallback(this);
        }

        public void OnDiscard()
        {
            CardRef.SetGameCard(null);
            CardRef = null;
        }

        public void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
