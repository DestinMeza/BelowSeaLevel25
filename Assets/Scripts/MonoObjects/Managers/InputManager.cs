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
        public delegate void OnPlayCard();
        public static OnPlayCard OnPlayCardCallback = delegate { };

        private bool hasMouseInput;
        private bool hasKeyboardInput;

        private void Update()
        {
            hasMouseInput = Input.GetMouseButtonDown(0);
            hasKeyboardInput = Input.GetButtonDown("Submit");

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
    }
}