using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.XR;
using System;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This object is the root of the Game Code
    /// </summary>
    internal class InputManager : MonoManager<InputManager>
    {
        public enum InputState
        {
            FreePointer,
            HandMode,
            SelectDiscard
        }

        private InputState inputState;

        private bool hasMouseInput;
        private bool hasKeyboardInput;

        private void Update()
        {
            hasMouseInput = Input.GetMouseButtonDown(0);
            hasKeyboardInput = Input.GetButtonDown("Submit");

            switch (inputState)
            {
                case InputState.FreePointer:
                    FreePointerState();
                    break;
                case InputState.HandMode:
                    HandModeState();
                    break;
                case InputState.SelectDiscard:
                    SelectDiscardState();
                    break;
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
                if (TryQueryMouseRaycast(x => x.collider.GetComponentInParent<MonoButton>() != null, out var hitObjects))
                {
                    MonoButton buttonClicked = hitObjects.First().collider.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }
            }
        }

        public void HandModeState()
        {
            if (hasMouseInput)
            {
                if (TryQueryMouseRaycast(x => x.collider.GetComponentInParent<MonoButton>() != null, out var hitButtons))
                {
                    MonoButton buttonClicked = hitButtons.First().collider.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }

                if (TryQueryMouseRaycast(x => x.collider.GetComponentInParent<MonoCard>() != null, out var hitCards))
                {
                    MonoCard selectedCard = hitCards.First().collider.GetComponentInParent<MonoCard>();
                    selectedCard.OnActivate();
                    return;
                }
            }
        }

        public void SelectDiscardState()
        {
            if (hasMouseInput)
            {
                if (TryQueryMouseRaycast(x => x.collider.GetComponentInParent<MonoButton>() != null, out var hitButtons))
                {
                    MonoButton buttonClicked = hitButtons.First().collider.GetComponent<MonoButton>();
                    buttonClicked.OnInteract();
                    return;
                }

                if (TryQueryMouseRaycast(x => x.collider.GetComponentInParent<MonoCard>() != null, out var hitObjects))
                {
                    MonoCard selectedCard = hitObjects.First().collider.GetComponentInParent<MonoCard>();
                    selectedCard.OnDiscard();

                    inputState = InputState.FreePointer;
                    return;
                }
            }
        }

        public bool TryQueryMouseRaycast(Func<RaycastHit2D, bool> query, out List<RaycastHit2D> hitEvents)
        {
            List<RaycastHit2D> hitObjects = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero).ToList();
            hitEvents = hitObjects.Where(query).ToList();

            return hitEvents.Count != 0;
        }


        public static void ForceDiscard()
        {
            Instance.inputState = InputState.SelectDiscard;
        }
    }
}