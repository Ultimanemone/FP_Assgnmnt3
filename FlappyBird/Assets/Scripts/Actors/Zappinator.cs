using Flappy_Assgnmt3.Core;
using UnityEngine;
using UnityEngine.VFX;

namespace Flappy_Assgnmt3.Actors
{
    public class Zappinator : Obstacle
    {
        private VisualEffect _effect;

        private void Awake()
        {
            _effect = GetComponent<VisualEffect>();
        }

        public void Init(Vector3 start, Vector3 end, int size = 10, float noiseOffset = 0.05f, float width = 0.01f)
        {
            _effect.SetInt("size", size);
            _effect.SetFloat("noiseOffset", noiseOffset);
            _effect.SetFloat("width", width);
            _effect.SetVector3("start", start);
            _effect.SetVector3("end", end);
        }

        protected override void OnFixedUpdate()
        {
            if (_effect != null && !Utils.IsWithinBounds(transform.localPosition))
            {
                _pool.Return(gameObject);
            }
        }
    }
}
