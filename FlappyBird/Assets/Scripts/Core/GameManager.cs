using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flappy_Assgnmt3.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public string CurrentScene { get; private set; }
        public bool IsPaused { get; private set; }
        [SerializeField] private AudioPlayer audioPlayer;

        private GameManager() { }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError($"Duplicate GameManager on {gameObject.name}!");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(audioPlayer.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;

#if !UNITY_EDITOR
            LoadScene("Game");
#endif
        }

        private void OnDestroy()
        {
            if (Instance == this)
                SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            CurrentScene = scene.name;
            IsPaused = false;
            Time.timeScale = 1f;
        }

        /// <summary>
        /// Load a scene
        /// </summary>
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
            Instance.CurrentScene = scene;
        }

        /// <summary>
        /// Load the next scene in build settings
        /// </summary>
        public void LoadNextScene()
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex + 1);
        }

        /// <summary>
        /// Reload the current scene
        /// </summary>
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Pause the game
        /// </summary>
        public void SetPaused(bool paused)
        {
            IsPaused = paused;
            Time.timeScale = paused ? 0f : 1f;
        }

        /// <summary>
        /// Toggle pausing
        /// </summary>
        public void TogglePause()
        {
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0f : 1f;
        }

        /// <summary>
        /// Quit the game
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("Game Quit");
            Application.Quit();
        }

        private void Update()
        {
            Scene scene = SceneManager.GetActiveScene();
        }
    }
}