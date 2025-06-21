using UnityEngine;

namespace BelowSeaLevel_25
{
    internal interface IManageable
    {
        //Init is the setup call for all manageable
        public void Init();
    }


    /// <summary>
    /// This class sets up all the boiler plate for other Manager objects
    /// </summary>
    internal abstract class Manager : MonoBehaviour
    {
        public abstract void Init();
    }

    internal abstract class MonoManager<T> : Manager where T : Manager
    {
        public static T Instance { get; private set; }
        public override void Init()
        {
            if (Instance != null)
            {
                Debug.LogError($"Tried Creating a new Instance of Manager {typeof(T).Name}");
                return;
            }

            Instance = this as T;

            Debug.Log($"Initalize {typeof(T).Name}");
        }

    }
}