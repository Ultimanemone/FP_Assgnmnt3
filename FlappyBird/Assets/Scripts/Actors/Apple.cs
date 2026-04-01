using Flappy_Assgnmt3.Core;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

namespace Flappy_Assgnmt3.Actors
{
    public class Apple : IdlePooledObj
    {
        protected BoxCollider2D _collider;
        private ParticleSystem _ps;
        private SpriteRenderer _sr;
        private bool _eaten;

        protected void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _ps = GetComponent<ParticleSystem>();
            _sr = GetComponent<SpriteRenderer>();
        }

        public void Init(Vector3 pos)
        {
            transform.localPosition = pos;
            _sr.color = Color.white;
            _eaten = false;
        }

        protected override void OnFixedUpdate()
        {
            if (!Utils.IsWithinBounds(transform.localPosition))
            {
                _pool.Return(gameObject);
            }
        }

        protected override void OnPlayerHit(GameObject player)
        {
            if (_eaten) return;
            BattleSceneManager.Instance.AddScore(2f);
            _sr.color = Color.clear;
            _ps.Play();
            _eaten = true;
            StartCoroutine(ReturnCR());
            Debug.Log("apple");
        }

        private IEnumerator ReturnCR()
        {
            yield return new WaitForSeconds(1f);
            _pool.Return(gameObject);
            yield break;
        }

        protected override void Move()
        {
            Vector3 pos = transform.localPosition;
            pos.x -= BattleSceneManager.Instance.Speed * Time.deltaTime;
            transform.localPosition = pos;
        }
    }
}
