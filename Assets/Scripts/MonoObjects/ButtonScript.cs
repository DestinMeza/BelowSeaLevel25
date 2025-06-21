using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BelowSeaLevel_25
{
    public abstract class MonoButton : MonoBehaviour
    {
        public abstract void OnInteract();
    }
    
    public class ButtonScript : MonoButton
    {
        public UnityEvent unityAction;

        public override void OnInteract()
        {
            unityAction.Invoke();
        }
    }
}
