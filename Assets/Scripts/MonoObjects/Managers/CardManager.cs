using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

using static BelowSeaLevel_25.Globals.Enums;

namespace BelowSeaLevel_25
{
    internal class CardManager : MonoManager<CardManager>
    {
        public const float MAX_POWER_VALUE = 100.0f;
        public float PowerValue = 0.0f;
        public float DrawCooldown = 3.0f;
        public float PowerBarConsumeRate = 1.0f;


        public MonoCard ActiveCard => m_ActiveCard;
        public MonoHand GameHand;

        private bool m_IsPowerModeActive;
        private MonoCard m_ActiveCard;
        private Coroutine m_AutoActivationCoroutine;
        private Coroutine m_ActiveConsumeBarCoroutine;

        private bool HasPowerCard => PowerValue >= MAX_POWER_VALUE;
        private bool CanDrawCard => m_IsPowerModeActive || m_CurrentDrawCooldown <= 0;
        private float m_CurrentDrawCooldown = 0;
        private int m_CurrentCount = 0;
        public bool IsShowCooldown => m_CurrentDrawCooldown > 0 && !m_IsPowerModeActive;
        public float GetCurrentDrawCooldown() => m_CurrentDrawCooldown;

        public override void Init()
        {
            base.Init();
            GameHand.Init();

            InputManager.OnPlayCardCallback += OnPlayCardCallback;
        }

        private void Update()
        {
            SubDrawCooldown();
        }

        private void SubDrawCooldown()
        {
            if (m_CurrentDrawCooldown > 0.0f)
            {
                m_CurrentDrawCooldown -= Time.deltaTime;

                UIManager.UpdateDrawCooldown();
            }
        }

        public void OnPlayCardCallback()
        {
            if (m_ActiveCard == null)
            {
                return;
            }

            if (HasPowerCard)
            {
                m_IsPowerModeActive = true;

                if (m_ActiveConsumeBarCoroutine != null)
                {
                    StopCoroutine(m_ActiveConsumeBarCoroutine);
                }

                StartCoroutine(ConsumePowerBar());
            }

            AudioManager.PlaySFXClip("Discard");

            if (m_ActiveCard.CardRef.AutoLeftClickLock)
            {
                if (m_AutoActivationCoroutine != null)
                {
                    StopCoroutine(m_AutoActivationCoroutine);
                }

                m_AutoActivationCoroutine = StartCoroutine(AutoActivation());
            }
            else
            {
                PlayActiveCard();
            }
        }

        public void PlayActiveCard()
        {
            if (m_ActiveCard == null)
            {
                Debug.LogError("Tried playing a card when there is non actively selected.");
                return;
            }

            m_ActiveCard.CardRef.OnActivate();
            m_CurrentCount++;

            int remainingActivations = m_ActiveCard.CardRef.GetCount() - m_CurrentCount;

            Debug.Log($"Card Reactivate Remaining: {remainingActivations}");

            if (remainingActivations <= 0)
            {
                GameHand.Discard(m_ActiveCard);
                m_ActiveCard = null;

                UIManager.SetUIState(UIState.HandMode);
                m_CurrentCount = 0;
            }
        }

        public IEnumerator AutoActivation()
        {
            do
            {
                PlayActiveCard();

                if (m_CurrentCount <= 0)
                {
                    break;
                }

                yield return new WaitForSeconds(m_ActiveCard.CardRef.AutoLeftClickRate);

            } while (m_CurrentCount > 0);

            m_AutoActivationCoroutine = null;
        }

        public static void AddPower(float powerAmount)
        {
            Instance.PowerValue += powerAmount;

            if (Instance.PowerValue >= MAX_POWER_VALUE)
            {
                Instance.PowerValue = Mathf.Min(MAX_POWER_VALUE, Instance.PowerValue);
            }

            UIManager.UpdatePower();
        }

        public static void SubPower(float powerAmount)
        {
            Instance.PowerValue -= powerAmount;
            Instance.PowerValue = Mathf.Max(0, Instance.PowerValue);

            if (Instance.PowerValue <= 0)
            {
                Instance.m_IsPowerModeActive = false;

                if (Instance.m_ActiveConsumeBarCoroutine != null)
                {
                    Instance.StopCoroutine(Instance.m_ActiveConsumeBarCoroutine);
                }
            }

            UIManager.UpdatePower();
        }

        public static void Draw()
        {
            if (!Instance.CanDrawCard)
            {
                return;
            }

            Instance.m_CurrentDrawCooldown = Instance.DrawCooldown;

            AudioManager.PlaySFXClip("Draw");
            Instance.GameHand.Draw();
            Instance.GameHand.Draw();
            Instance.GameHand.Draw();
        }

        public static void SetActiveCard(MonoCard card)
        {
            if (Instance.m_AutoActivationCoroutine != null)
            {
                Instance.StopCoroutine(Instance.m_AutoActivationCoroutine);
            }

            Instance.m_CurrentCount = 0;
            Instance.m_ActiveCard = card;
            UIManager.SetUIState(UIState.PlayCardMode);
        }

        public static void External_PlayActiveCard()
        {
            UIManager.SetUIState(UIState.FreePointer);
        }

        public static void External_CancelActiveCard()
        {
            MonoCard monoCard = Instance.m_ActiveCard;
            Instance.GameHand.Discard(monoCard);
            Instance.m_IsPowerModeActive = false;
            Instance.m_ActiveCard = null;
            Instance.m_CurrentCount = 0;
            UIManager.SetUIState(UIState.HandMode);

            if (Instance.m_AutoActivationCoroutine != null)
            {
                Instance.StopCoroutine(Instance.m_AutoActivationCoroutine);
            }

            if (Instance.m_ActiveConsumeBarCoroutine != null)
            {
                Instance.StopCoroutine(Instance.m_ActiveConsumeBarCoroutine);
            }
        }

        private IEnumerator ConsumePowerBar()
        {
            m_IsPowerModeActive = true;

            while (m_IsPowerModeActive && enabled)
            {
                for (float t = 0; t < 1.0f; t += Time.deltaTime)
                {
                    SubPower(Time.deltaTime * PowerBarConsumeRate);
                    yield return new WaitForEndOfFrame();
                }
            }

            m_ActiveConsumeBarCoroutine = null;
            m_IsPowerModeActive = false;
        }
        
        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}