using System.Collections.Generic;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class PooledObj : MonoBehaviour
    {
        protected ObjPool _pool;

        public void SetPool(ObjPool pool)
        {
            _pool = pool;
        }
    }

    public class ObjPool
    {
        private Queue<GameObject> _inactive = new Queue<GameObject>();
        private List<GameObject> _pool = new List<GameObject>();
        private GameObject _prefab;
        private Transform _root;
        public IReadOnlyList<GameObject> Pool => _pool;

        public ObjPool(GameObject prefab, int initSize = 20, Transform root = null)
        {
            _prefab = prefab;
            _root = root;
            

            for (int i = 0; i < initSize; i++)
            {
                GameObject obj = Object.Instantiate(prefab, root);
                _pool.Add(obj);
                _inactive.Enqueue(obj);
                obj.SetActive(false);
                obj.GetComponent<PooledObj>()?.SetPool(this);
            }
        }

        public bool TryGet(out GameObject obj)
        {
            if (_inactive.Count > 0)
            {
                obj = _inactive.Dequeue();
                obj.SetActive(true);
                return true;
            }
            else
            {
                // dynamic pooling
                obj = Object.Instantiate(_prefab, _root);
                obj.SetActive(true);
                return false;
            }
        }

        public void Return(GameObject obj)
        {
            _inactive.Enqueue(obj);
            obj.SetActive(false);
        }
    }
}
