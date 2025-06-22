using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BelowSeaLevel_25.Globals.Enums;

namespace BelowSeaLevel_25
{
    internal class UIManager : MonoManager<UIManager>
    {
        public static UIState State => Instance.uiState;
        public UIState uiState = UIState.FreePointer;

        public MonoHand Hand;
        public ButtonScript DrawButton;
        public ButtonScript CloseHandButton;
        public ButtonScript OpenHandButton;
        public ButtonScript CancelPlayButton;

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
    }
}