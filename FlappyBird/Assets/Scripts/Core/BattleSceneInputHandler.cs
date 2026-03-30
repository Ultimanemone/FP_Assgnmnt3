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
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                BattleSceneManager.instance.TogglePauseMenu();
            }
        }

        private void FixedUpdate()
        {

        }
    }
}