using Flappy_Assgnmt3.Actors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Flappy_Assgnmt3.Core
{
    public class BattleSceneInputHandler : MonoBehaviour
    {
        private static BattleSceneInputHandler _instance;


        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Update()
        {
            bool flag0 = BattleSceneManager.Instance.State == BattleSceneState.Playing;
            bool flag1 = BattleSceneManager.Instance.State == BattleSceneState.Paused;
            if (Keyboard.current.escapeKey.wasPressedThisFrame && (flag0 || flag1))
            {
                BattleSceneManager.Instance.TogglePauseMenu();
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame && flag0)
            {
                Player.instance.Flap();
            }

            if (Keyboard.current.enterKey.wasPressedThisFrame && BattleSceneManager.Instance.State == BattleSceneState.Result)
            {
                GameManager.Instance.LoadScene("Game");
            }
        }

        private void FixedUpdate()
        {

        }
    }
}