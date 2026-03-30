using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class IdlePooledObj : PooledObj
    {
        protected void FixedUpdate()
        {
            OnFixedUpdate();
            Move();
        }

        protected virtual void OnFixedUpdate() { }

        protected virtual void Move()
        {
            Vector3 pos = transform.localPosition;
            pos.x -= BattleSceneManager.instance.speed * Time.deltaTime;
            transform.localPosition = pos;
        }
    }
}
