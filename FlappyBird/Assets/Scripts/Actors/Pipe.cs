using Flappy_Assgnmt3.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

namespace Flappy_Assgnmt3.Actors
{
    public class Pipe : IdlePooledObj
    {
        protected LineRenderer _lr;
        protected SpriteRenderer _cap;
        protected BoxCollider2D _collider;

        protected void Awake()
        {
            _lr = GetComponent<LineRenderer>();
            _cap = GetComponentInChildren<SpriteRenderer>();
            _collider = GetComponent<BoxCollider2D>();
        }

        public void Init(Vector3 start, Vector3 end, Vector3 capPos, bool faceUp = true)
        {
            _lr.SetPosition(0, start);
            _lr.SetPosition(1, end);
            _cap.transform.localPosition = capPos;
            if (!faceUp) _cap.transform.localScale = new Vector3(1f, -1f, 1f);
            _collider.size = new Vector2(0.625f, (start - end).magnitude);
            Vector2 offset = (Vector2)(start + end) / 2f;
            offset.x = 0f;
            _collider.offset = offset;
        }

        protected override void OnFixedUpdate()
        {
            if (_lr != null && !Utils.IsWithinBounds(transform.localPosition))
            {
                _pool.Return(gameObject);
            }
        }

        protected override void Move()
        {
            Vector3 pos = transform.localPosition;
            pos.x -= BattleSceneManager.Instance.Speed * Time.deltaTime;
            transform.localPosition = pos;
        }
    }
}
