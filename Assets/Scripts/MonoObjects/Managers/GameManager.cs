using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BelowSeaLevel_25.Globals.GameState;

namespace BelowSeaLevel_25
{
    public class Player
    {
        public int Health = 10;

        public int Score => m_Score;
        public int CurrentHealth => m_CurrentHealth;

        private int m_CurrentHealth;
        private int m_Score;

        public Player()
        {
            Reset();
        }

        public void Reset()
        {
            m_CurrentHealth = Health;
            m_Score = 0;
        }

        public void AddScore(int score)
        {
            m_Score += score;
            UIManager.UpdateScore();
        }

        public void SubHealth(int health)
        {
            m_CurrentHealth -= health;
            UIManager.UpdateHealth();

            if (GameManager.Player.CurrentHealth <= 0)
            {
                GameManager.GameOver();
            }
        }
    }

    /// <summary>
    /// This object is the root of the Game Code
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static Player Player;
        public Transform CannonStartPos;

        private List<Manager> managers;

        /// <summary>
        /// This is the function that is found by Unity
        /// </summary>
        public void Start()
        {
            Instance = this;
            Player = new Player();

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

        public static void GameOver()
        {
            IsPlaying = false;
        }

        //This is a pretty hacky reset, but it's game jam code so whatever D:<
        public static void Reset()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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