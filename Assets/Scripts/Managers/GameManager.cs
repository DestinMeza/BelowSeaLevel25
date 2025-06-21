using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This object is the root of the Game Code
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        private List<Manager> managers;

        /// <summary>
        /// This is the function that is found by Unity
        /// </summary>
        public void Start()
        {
            Instance = this;

            SetupManagers();

            Init();
        }

        private void SetupManagers()
        {
            Instance.managers = new List<Manager>()
            {
                GetComponent<DebugManager>(),
                GetComponent<AudioManager>()
            };
        }

        private void Init()
        {
            managers.ForEach(x => x.Init());
        }
    }  
}