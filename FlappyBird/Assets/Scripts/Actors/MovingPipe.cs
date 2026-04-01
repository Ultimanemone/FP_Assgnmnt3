using Flappy_Assgnmt3.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

namespace Flappy_Assgnmt3.Actors
{
    public class MovingPipe : Pipe
    {
        // original pos
        private Vector3 _start;
        private Vector3 _end;

        private float _offset;
        private bool _faceUp;
        private float _lifetime;

        public void Init(Vector3 start, Vector3 end, Vector3 capPos, float movingDist, bool faceUp = true)
        {
            _start = start;
            _end = end;
            _offset = movingDist;
            _faceUp = faceUp;
            _lifetime = 0f;

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

            MoveVertical();
        }

        private void MoveVertical()
        {
            _lifetime += Time.deltaTime;
            Vector3 offset = Vector3.up * Mathf.Sin(_lifetime) * _offset;
            Vector3 temp = transform.localPosition;
            temp.y = offset.y;
            transform.localPosition = temp;
        }
    }
}
