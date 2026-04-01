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
        [SerializeField] private AudioSource _music;
        [SerializeField] private AudioSource _sfx;
        private bool _isPlaying;
        private float _sfxVol = 1f;

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
            DontDestroyOnLoad(_music);

            SceneManager.sceneLoaded += OnSceneLoaded;
#if !UNITY_EDITOR
            LoadScene("Title");
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
            _music.Stop();
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

        public void PlayMusic(bool play)
        {
            if (play)
            {
                _music.Play();
                _isPlaying = true;
            }
            else
            {
                _music.Stop();
                _isPlaying = false;
            }
        }

        public void ToggleMusic()
        {
            if (_isPlaying)
            {
                _music.Pause();
                _isPlaying = false;
            }
            else
            {
                _music.UnPause();
                _isPlaying = true;
            }
        }

        public void PlaySFX(AudioClip audioClip, float volume = 1f)
        {
            AudioSource source = Instantiate(_sfx);
            source.clip = audioClip;
            source.volume = _sfxVol * volume;
            source.Play();
            Destroy(source.gameObject, audioClip.length + 1f);
            //Destroy(source.gameObject, 3f);
        }

        private void Update()
        {
            Scene scene = SceneManager.GetActiveScene();
            _music.volume = SettingsData.instance.musicVol / 4f * 0.8f;
            _sfxVol = SettingsData.instance.sfxVol / 4f;
        }
    }
}