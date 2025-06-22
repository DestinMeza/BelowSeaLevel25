using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BelowSeaLevel_25
{
    public abstract class MonoButton : MonoBehaviour, IUIElement
    {
        public abstract void OnInteract();
        public abstract void SetActive(bool state);
    }

    public class ButtonScript : MonoButton
    {
        public UnityEvent unityAction;

        public override void OnInteract()
        {
            unityAction.Invoke();
        }

        public override void SetActive(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}
