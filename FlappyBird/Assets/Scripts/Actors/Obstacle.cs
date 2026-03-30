using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class Obstacle : MonoBehaviour
    {
        protected ObjPool _pool;

        public void SetPool(ObjPool pool)
        {
            _pool = pool;
        }

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
