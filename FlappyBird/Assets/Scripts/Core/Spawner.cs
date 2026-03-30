using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class Spawner : MonoBehaviour
    {
        private float _prev;
        private float _counter;
        private ObjPool _pool;
        [SerializeField] private GameObject _zappinator;

        private void Awake()
        {
            _prev = 0f;
            _counter = 0f;
            _pool = new ObjPool(_zappinator, 40, transform);
            foreach (GameObject obj in _pool.Pool)
            {
                obj.GetComponent<Zappinator>()?.SetPool(_pool);
            }
        }

        private void FixedUpdate()
        {
            _counter += BattleSceneManager.instance.speed * Time.deltaTime;
            if (_counter > 3f)
            {
                Spawn();
                _counter = 0f;
            }
        }

        private void Spawn()
        {
            _prev = _prev + Random.Range(-1f, 1f);
            float halfGap = Random.Range(1f, 2f) / 2f;

            if (_pool.TryGet(out GameObject arcTop))
            {
                Zappinator arc = arcTop.GetComponent<Zappinator>();
                Vector3 start = Vector3.zero + Vector3.up * halfGap;
                Vector3 end = Vector3.up * 3.75f;
                arc.Init(start, end);
                arcTop.transform.localPosition = new Vector3(5f, 0, 0f);

                BoxCollider2D hitbox = arcTop.GetComponent<BoxCollider2D>();
                hitbox.offset = start + (end - start) / 2f;
                hitbox.size = new Vector2(0.5f, end.y - start.y);
            }

            if (_pool.TryGet(out GameObject arcBottom))
            {
                Zappinator arc = arcBottom.GetComponent<Zappinator>();
                Vector3 start = Vector3.zero - Vector3.up * halfGap;
                Vector3 end = Vector3.up * -3.75f;
                arc.Init(start, end);
                arcBottom.transform.localPosition = new Vector3(5f, 0, 0f);

                BoxCollider2D hitbox = arcBottom.GetComponent<BoxCollider2D>();
                hitbox.offset = start + (end - start) / 2f;
                hitbox.size = new Vector2(0.5f, start.y - end.y);
            }

        }
    }
}