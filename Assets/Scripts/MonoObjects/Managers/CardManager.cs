using System.Collections.Generic;
using System.Collections;
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
        private int m_CurrentCount = 0;

        public override void Init()
        {
            base.Init();
            GameHand.Init();

            InputManager.OnPlayCardCallback += OnPlayCardCallback;
        }

        public void OnPlayCardCallback()
        {
            if (m_ActiveCard.AutoLeftClickLock)
            {
                StartCoroutine(AutoActivation());
            }

            PlayActiveCard();
        }

        public void PlayActiveCard()
        { 
            if (m_ActiveCard == null)
            {
                Debug.LogError("Tried playing a card when there is non actively selected.");
                return;
            }

            AudioManager.PlaySFXClip("Discard");

            m_ActiveCard.OnActivate();
            m_CurrentCount++;

            int remainingActivations = m_ActiveCard.GetCount() - m_CurrentCount;

            Debug.Log($"Card Reactivate Remaining: {remainingActivations}");

            if (0 >= remainingActivations)
            {
                MonoCard monoCard = m_ActiveCard.MonoCard;
                GameHand.Discard(monoCard);
                m_ActiveCard = null;

                UIManager.SetUIState(UIState.HandMode);
                m_CurrentCount = 0;
            }
        }
        
        public IEnumerator AutoActivation()
        {
            do
            {
                yield return new WaitForSeconds(m_ActiveCard.AutoLeftClickRate);

                if (m_CurrentCount <= 0)
                {
                    break;
                }

                PlayActiveCard();

            } while (m_CurrentCount > 0);
        }

        public static void Draw()
        {
            AudioManager.PlaySFXClip("Draw");
            Instance.GameHand.Draw();
            Instance.GameHand.Draw();
            Instance.GameHand.Draw();
        }

        public static void SetActiveCard(Card card)
        {
            Instance.StopCoroutine(Instance.AutoActivation());

            Instance.m_ActiveCard = card;
            UIManager.SetUIState(UIState.PlayCardMode);
        }

        public static void External_PlayActiveCard()
        {
            UIManager.SetUIState(UIState.FreePointer);
        }

        public static void External_CancelActiveCard()
        {
            UIManager.SetUIState(UIState.HandMode);
        }
    }
}