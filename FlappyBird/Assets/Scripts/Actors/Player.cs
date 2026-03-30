using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class Player : MovingObj
    {
        private Vector3 speed;
        public static Player instance { get; private set; }

        private Player() { }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            speed = Vector2.zero;
        }

        protected override void FixedUpdate()
        {
            if (BattleSceneManager.instance.state == BattleSceneState.Playing)
            {
                speed.y = Mathf.Max(speed.y - 7f * Time.deltaTime, -5f);
                Vector3 newPos = transform.localPosition;
                newPos.y = Mathf.Clamp(newPos.y + speed.y * Time.deltaTime, -3.75f, 3.75f);
                transform.localPosition = newPos;
            }
        }

        public void Flap()
        {
            speed.y = 3f;
        }
    }
}
