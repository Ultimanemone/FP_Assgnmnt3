using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class IdlePooledObj : PooledObj
    {
        protected void FixedUpdate()
        {
            if (BattleSceneManager.Instance.State == BattleSceneState.Result) return;
            OnFixedUpdate();
            Move();
        }

        protected virtual void OnFixedUpdate() { }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name.Contains("Player") && BattleSceneManager.Instance.State == BattleSceneState.Playing)
            {
                OnPlayerHit(other.gameObject);
            }
        }

        protected virtual void OnPlayerHit(GameObject player)
        {
            BattleSceneManager.Instance.Finish();
        }

        protected virtual void Move()
        {
            Vector3 pos = transform.localPosition;
            pos.x -= BattleSceneManager.Instance.Speed * Time.deltaTime;
            transform.localPosition = pos;
        }
    }
}
