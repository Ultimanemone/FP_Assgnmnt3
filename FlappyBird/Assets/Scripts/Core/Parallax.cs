using Flappy_Assgnmt3.Actors;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Flappy_Assgnmt3.Core
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private float _parallaxEffect;
        private GameObject _sprite0;
        private GameObject _sprite1;
        private GameObject _sprite2;
        private GameObject _rightmost;
        private Queue<GameObject> _queue;

        private void Awake()
        {
            _sprite0 = GetComponentsInChildren<SpriteRenderer>().First(x => x.name.Contains("sprite")).gameObject;
            _sprite1 = GetComponentsInChildren<SpriteRenderer>().First(x => x.name.Contains("sprite (1)")).gameObject;
            _sprite2 = GetComponentsInChildren<SpriteRenderer>().First(x => x.name.Contains("sprite (2)")).gameObject;
            _queue = new Queue<GameObject>();
            _queue.Enqueue(_sprite0);
            _queue.Enqueue(_sprite1);
            _queue.Enqueue(_sprite2);
            _rightmost = _sprite2;
        }

        private void Update()
        {
            if (BattleSceneManager.Instance.State == BattleSceneState.Result) return;

            bool flag = _queue.Peek().transform.localPosition.x <= -20f;
            if (flag)
            {
                GameObject temp = _queue.Dequeue();
                Vector3 pos = _rightmost.transform.localPosition;
                pos.x += 16f;
                temp.transform.localPosition = pos;
                _rightmost = temp;
                _queue.Enqueue(temp);
            }

            UpdatePos(_sprite0);
            UpdatePos(_sprite1);
            UpdatePos(_sprite2);
        }

        private void UpdatePos(GameObject obj, bool left = true)
        {
            Vector3 pos = obj.transform.position;
            pos.x += (left ? -1f : 1f) * _parallaxEffect * BattleSceneManager.Instance.Speed * Time.deltaTime * _parallaxEffect;
            obj.transform.position = pos;
        }
    }
}