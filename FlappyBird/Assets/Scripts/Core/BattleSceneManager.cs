using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public enum BattleSceneState
    {
        Playing,
        Paused
    }

    public class BattleSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pausedMenu;

        public static BattleSceneManager instance { get; private set; }
        public BattleSceneState state { get; private set; }
        public float speed { get; private set; }
        private float _timer;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            _pausedMenu.SetActive(false);
        }

        private void Start()
        {
            GameManager.instance.PlayMusic(true);
            _timer = 0f;
            state = BattleSceneState.Playing;
            speed = 1f;
        }

        private void FixedUpdate()
        {

        }

        private void Update()
        {

        }

        public void TogglePauseMenu()
        {
            bool pauseState = GameManager.instance.isPaused;
            _pausedMenu.SetActive(!pauseState);

            if (!pauseState)
            {
                state = BattleSceneState.Paused;
            }
            else
            {
                state = BattleSceneState.Playing;
            }

            GameManager.instance.SetPaused(!pauseState);
        }

        public void QuitToTitle()
        {
            GameManager.instance.LoadScene("Title");
        }

        public void Finish()
        {

        }
    }
}