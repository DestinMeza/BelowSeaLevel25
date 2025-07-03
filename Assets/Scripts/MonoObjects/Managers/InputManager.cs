using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static BelowSeaLevel_25.Globals.Enums;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This object is the root of the Game Code
    /// </summary>
    internal class InputManager : MonoManager<InputManager>
    {
        public EventSystem eventSystem;
        public delegate void OnPlayCard();
        public static OnPlayCard OnPlayCardCallback = delegate { };

        private bool hasMouseInput;
        private bool hasKeyboardSubmitInput;
        private int keyboardInputIndex = -1;


        private void Update()
        {
            keyboardInputIndex = -1;
            {
                int index = 0;
                for (KeyCode keyCode = KeyCode.Alpha1; keyCode <= KeyCode.Alpha9; keyCode++)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        keyboardInputIndex = index;
                        break;
                    }
                    index++;
                }
            }

            hasMouseInput = Input.GetMouseButtonDown(0);
            hasKeyboardSubmitInput = Input.GetButtonDown("Submit");

            if (!Globals.GameState.IsPlaying && GameManager.Instance.CanInputRestart && (hasMouseInput || hasKeyboardSubmitInput))
            {
                GameManager.Reset();
                return;
            }

            switch (UIManager.State)
            {
                case UIState.FreePointer: FreePointerState(); return;
                case UIState.HandMode: HandModeState(); return;
                case UIState.PlayCardMode: PlayCardState(); return;
                case UIState.SelectDiscard: SelectDiscardState(); return;
            }
        }

        public void FreePointerState()
        {
            //Action

            //Hand

            //Query mouse click for hitting button that shows hand.

            //Query submit button to shows hand.

            if (hasMouseInput)
            {
                if (TryQueryMouseRaycast(x => x.gameObject.GetComponent<MonoButton>() != null, out var hitObjects))
                {
                    Debug.Log("Hit Button!");
                    MonoButton buttonClicked = hitObjects.First().gameObject.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }
            }
            if (hasKeyboardSubmitInput)
            {
                if (eventSystem.currentSelectedGameObject != null && eventSystem.currentSelectedGameObject.TryGetComponent(out ButtonScript buttonScript))
                {
                    Debug.Log($"Selected Button! :{buttonScript.gameObject.name}");
                    buttonScript.OnInteract();
                    return;
                }
            }
        }

        public void HandModeState()
        {
            if (hasMouseInput)
            {
                if (TryQueryMouseRaycast(x => x.gameObject.GetComponent<MonoButton>() != null, out var hitButtons))
                {
                    MonoButton buttonClicked = hitButtons.First().gameObject.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }

                if (TryQueryMouseRaycast(x => x.gameObject.GetComponentInParent<MonoCard>() != null, out var hitCards))
                {
                    MonoCard selectedCard = hitCards.First().gameObject.GetComponentInParent<MonoCard>();
                    selectedCard.OnActivate();
                    return;
                }
            }
            if (hasKeyboardSubmitInput)
            {
                if (eventSystem.currentSelectedGameObject != null && eventSystem.currentSelectedGameObject.TryGetComponent(out ButtonScript buttonScript))
                {
                    buttonScript.OnInteract();
                    return;
                }
                if (eventSystem.currentSelectedGameObject != null && eventSystem.currentSelectedGameObject.TryGetComponent(out MonoCard monoCard))
                {
                    monoCard.OnActivate();
                    return;
                }
            }
            if (keyboardInputIndex != -1)
            {
                MonoCard monoCard = UIManager.Instance.Hand.GetAtActiveIndex(keyboardInputIndex);

                if (monoCard == null)
                {
                    return;
                }

                monoCard.OnActivate();
                return;
            }

        }

        public void PlayCardState()
        {
            if (hasMouseInput)
            {
                if (TryQueryMouseRaycast(x => x.gameObject.GetComponent<MonoButton>() != null, out var hitObjects))
                {
                    Debug.Log("Hit Button!");
                    MonoButton buttonClicked = hitObjects.First().gameObject.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }

                OnPlayCardCallback();
            }
            if (hasKeyboardSubmitInput)
            {
                if (eventSystem.currentSelectedGameObject != null && eventSystem.currentSelectedGameObject.TryGetComponent(out ButtonScript buttonScript))
                {
                    buttonScript.OnInteract();
                    return;
                }
            }
        }

        public void SelectDiscardState()
        {
            if (hasMouseInput)
            {
                if (TryQueryMouseRaycast(x => x.gameObject.GetComponent<MonoButton>() != null, out var hitButtons))
                {
                    MonoButton buttonClicked = hitButtons.First().gameObject.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }

                if (TryQueryMouseRaycast(x => x.gameObject.GetComponentInParent<MonoCard>() != null, out var hitObjects))
                {
                    MonoCard selectedCard = hitObjects.First().gameObject.GetComponentInParent<MonoCard>();
                    selectedCard.OnDiscard();

                    UIManager.SetUIState(UIState.FreePointer);
                    return;
                }
            }
        }

        public bool TryQueryMouseRaycast(Func<RaycastResult, bool> query, out List<RaycastResult> hitResults)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.mousePosition;

            hitResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, hitResults);
            hitResults = hitResults.Where(query).ToList();

            Debug.Log($"Mouse Hit: {hitResults.Count}");

            return hitResults.Count > 0;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public static void SetActiveGameObject(GameObject gameObject)
        {
            Instance.eventSystem.SetSelectedGameObject(gameObject);
        }
    }
}