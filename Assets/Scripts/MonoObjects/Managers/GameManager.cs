using System.Collections.Generic;
using UnityEngine;
using static BelowSeaLevel_25.Globals.GameState;

namespace BelowSeaLevel_25
{
    /// <summary>
    /// This object is the root of the Game Code
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public Transform CannonStartPos;

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
                //Core
                GetComponent<DebugManager>(),
                GetComponent<AudioManager>(),
                GetComponent<InputManager>(),
                GetComponent<UIManager>(),

                //Game Specfic
                GetComponent<CardManager>(),
                GetComponent<EntityManager>()
            };
        }

        private void Init()
        {
            IsPlaying = true;
            PlayerStartingPosition = CannonStartPos.position;

            managers.ForEach(x => x.Init());

            //Queue Starting Sequence
            ActivePlayer = EntityManager.Spawn("Cannon", PlayerStartingPosition) as MonoCannonEntity;
        }
    }
}