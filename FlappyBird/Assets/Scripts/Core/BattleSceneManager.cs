using TMPro;
using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public enum BattleSceneState
    {
        Ready,
        Playing,
        Paused,
        Result
    }

    public class BattleSceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pausedMenu;
        [SerializeField] private GameObject _scoreMenu;
        [SerializeField] private GameObject _resultMenu;
        private TextMeshProUGUI _scoreText;

        public static BattleSceneManager Instance { get; private set; }
        public BattleSceneState State { get; private set; }
        private BattleSceneState _prevState;
        public float Speed
        {
            get
            {
                return _speedCurve.Evaluate(Timer);
            }
        }
        public float Difficulty
        {
            get
            {
                return _diffCurve.Evaluate(Timer);
            }
        }
        [SerializeField] private AnimationCurve _speedCurve;
        [SerializeField] private AnimationCurve _diffCurve;
        public float Score
        {
            get
            {
                return Mathf.Round(_score * 5f);
            }
        }
        private float _score;
        private float _timer;
        public float Timer
        {
            get
            {
                return _timer;
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _pausedMenu.SetActive(false);
            _resultMenu.SetActive(false);
            _scoreMenu.SetActive(false);
            _scoreText = _scoreMenu.GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            GameManager.Instance.PlayMusic(true);
            _timer = 0f;
            State = BattleSceneState.Ready;
        }

        private void FixedUpdate()
        {
            _timer += Time.deltaTime;
            if (State == BattleSceneState.Playing) _score += Time.deltaTime * Speed;

            if (_timer >= 3f && State != BattleSceneState.Playing && State != BattleSceneState.Result)
            {
                ChangeState(BattleSceneState.Playing);
                _scoreMenu.SetActive(true);
            }
            _scoreText.text = Score.ToString();
        }

        private void ChangeState(BattleSceneState newState)
        {
            if (newState != State)
            {
                _prevState = State;
                State = newState;
            }
        }

        private void RevertState()
        {
            BattleSceneState temp = State;
            State = _prevState;
            _prevState = temp;
        }

        public void TogglePauseMenu()
        {
            if (State != BattleSceneState.Result)
            {
                GameManager.Instance.TogglePause();
                bool pauseState = GameManager.Instance.IsPaused;
                _pausedMenu.SetActive(pauseState);
                _scoreMenu.SetActive(!pauseState);

                if (pauseState)
                {
                    ChangeState(BattleSceneState.Paused);
                }
                else
                {
                    RevertState();
                }
            }
        }

        public void AddScore(float score)
        {
            _score += score;
        }

        public void Finish()
        {
            ChangeState(BattleSceneState.Result);
            _resultMenu.SetActive(true);
            _resultMenu.GetComponent<Animator>().Play("finish");
            _scoreMenu.GetComponent<Animator>().Play("finish");
        }

        public static void QuitGame()
        {
            GameManager.Instance.QuitGame();
        }

        public static void RestartGame()
        {
            GameManager.Instance.LoadScene("Game");
        }
    }
}