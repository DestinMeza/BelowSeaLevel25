using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static BelowSeaLevel_25.Globals.Enums;

namespace BelowSeaLevel_25
{
    internal class CardManager : MonoManager<CardManager>
    {
        public Card ActiveCard => m_ActiveCard;
        public MonoHand GameHand;

        private Card m_ActiveCard;

        public override void Init()
        {
            base.Init();
            GameHand.Init();

            InputManager.OnPlayCardCallback += OnPlayCardCallback;
        }

        public void OnPlayCardCallback()
        {
            if (m_ActiveCard == null)
            {
                Debug.LogError("Tried playing a card when there is non actively selected.");
                return;
            }

            m_ActiveCard.OnActivate();
            GameHand.Discard(m_ActiveCard.MonoCard);
            m_ActiveCard = null;

            UIManager.SetUIState(UIState.HandMode);
        }

        public static void Draw()
        {
            Instance.GameHand.Draw();
        }

        public static void SetActiveCard(Card card)
        {
            Instance.m_ActiveCard = card;
            UIManager.SetUIState(UIState.PlayCardMode);
        }

        public static void PlayActiveCard()
        {
            UIManager.SetUIState(UIState.FreePointer);
        }

        public static void CancelActiveCard()
        {
            UIManager.SetUIState(UIState.HandMode);
        }
    }
}