using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BelowSeaLevel_25.Globals.Enums;

namespace BelowSeaLevel_25
{
    internal class UIManager : MonoManager<UIManager>
    {
        public Image HealthBarUI;
        public Image PowerBarUI;
        public TMPro.TextMeshProUGUI ScoreUI;
        public TMPro.TextMeshProUGUI GameOverUI;

        public static UIState State => Instance.uiState;
        public UIState uiState = UIState.FreePointer;

        public MonoHand Hand;
        public ButtonScript DrawButton;
        public ButtonScript CloseHandButton;
        public ButtonScript OpenHandButton;
        public ButtonScript CancelPlayButton;

        public TextMeshProUGUI DrawCooldownText;

        private IUIElement[] Interfaces;
        private Dictionary<UIState, IUIElement[]> UICollections;

        public override void Init()
        {
            base.Init();

            //Setup UI collections

            Interfaces = new IUIElement[]
            {
                Hand,
                DrawButton,
                CloseHandButton,
                OpenHandButton,
                CancelPlayButton
            };

            UICollections = new()
            {
                {
                    UIState.FreePointer,
                    new IUIElement[]
                    {
                        OpenHandButton
                    }
                },
                {
                    UIState.HandMode,
                    new IUIElement[]
                    {
                        CloseHandButton,
                        DrawButton,
                        Hand
                    }
                },
                {
                    UIState.PlayCardMode,
                    new IUIElement[]
                    {
                        CancelPlayButton
                    }
                },
                {
                    UIState.SelectDiscard,
                    new IUIElement[]
                    {
                        Hand
                    }
                }
            };
            
            UpdateUIState();
            SetKeyboardInputPointer();
        }

        public void UpdateUIState()
        {
            for (int i = 0; i < Interfaces.Length; i++)
            {
                bool isInCollection = Instance.UICollections[uiState].Contains(Interfaces[i]);

                Interfaces[i].SetActive(isInCollection);
            }
        }

        public static void SetUIState(UIState state)
        {
            Debug.Log($"UIState State: {state}");
            Instance.uiState = state;

            Instance.UpdateUIState();

            SetKeyboardInputPointer();
        }

        private static void SetKeyboardInputPointer()
        {
            switch (Instance.uiState)
            {
                case UIState.FreePointer:
                    InputManager.SetActiveGameObject(Instance.OpenHandButton.gameObject);
                    break;
                case UIState.HandMode:
                    if (Instance.Hand.StartingCard != null)
                    {
                        InputManager.SetActiveGameObject(Instance.Hand.StartingCard.gameObject);
                    }
                    else
                    {
                        InputManager.SetActiveGameObject(Instance.DrawButton.gameObject);
                    }
                    break;
                case UIState.PlayCardMode:
                    InputManager.SetActiveGameObject(null);
                    break;

                //Implement Me
                case UIState.SelectDiscard:

                    break;
            } 
        }

        public static void ForceDiscard()
        {
            SetUIState(UIState.SelectDiscard);
        }

        public static void ForceFreePointer()
        {
            SetUIState(UIState.FreePointer);
        }

        public static void ForceHandMode()
        {
            SetUIState(UIState.HandMode);
        }

        public static void ForceCancelPlay()
        {
            SetUIState(UIState.HandMode);
        }

        public static void UpdateHealth()
        {
            Instance.HealthBarUI.fillAmount = (float)GameManager.Player.CurrentHealth / (float)GameManager.Player.Health;
        }

        public static void UpdatePower()
        {
            Instance.PowerBarUI.fillAmount = (float)CardManager.Instance.PowerValue / (float)CardManager.MAX_POWER_VALUE;
        }

        public static void UpdateScore()
        {
            Instance.ScoreUI.text = GameManager.Player.Score.ToString();
        }

        public static void UpdateDrawCooldown()
        {
            float drawCooldown = CardManager.Instance.GetCurrentDrawCooldown();
            Instance.DrawCooldownText.gameObject.SetActive(CardManager.Instance.IsShowCooldown);
            Instance.DrawCooldownText.text = ((int)drawCooldown).ToString();
        }

        public static void DisplayGameOver()
        {
            Instance.GameOverUI.gameObject.SetActive(true);
        }

        public static void HideGameOver()
        { 
            Instance.GameOverUI.gameObject.SetActive(false);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}