using Flappy_Assgnmt3.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class Spawner : MonoBehaviour
    {
        private enum PipeType
        {
            Idle,
            Moving
        }

        private float _prev;
        private float _pipeCounter;
        private float _appleCounter;
        private ObjPool _idlePipePool;
        private ObjPool _movingPipePool;
        private ObjPool _applePool;
        [SerializeField] private GameObject _pipe;
        [SerializeField] private GameObject _movingPipe;
        [SerializeField] private GameObject _apple;

        private void Awake()
        {
            _prev = 0f;
            _pipeCounter = 3f;
            _appleCounter = 1.5f;
            _idlePipePool = new ObjPool(_pipe, 20, transform);
            _movingPipePool = new ObjPool(_movingPipe, 10, transform);
            _applePool = new ObjPool(_apple, 20, transform);
        }

        private void FixedUpdate()
        {
            if (BattleSceneManager.Instance.State == BattleSceneState.Playing)
            {
                float delta = BattleSceneManager.Instance.Speed * Time.deltaTime;
                _pipeCounter += delta;
                _appleCounter += delta;

                if (_pipeCounter > 3f)
                {
                    SpawnPipe();
                    _pipeCounter = 0f;
                }

                if (_appleCounter > 3f)
                {
                    SpawnApple();
                    _appleCounter = 0f;
                }
            }
        }

        private void SpawnApple()
        {
            int count = Random.Range(2, 6);

            float minY = -3f;
            float maxY = 3f;

            float objectHeight = 0.2f;
            float spacing = 0.5f;

            // Total height of the whole column
            float stackHeight = count * objectHeight + (count - 1) * spacing;

            // Make sure it fits
            if (stackHeight > (maxY - minY))
            {
                Debug.LogWarning("Stack too big for range!");
                return;
            }

            // Pick a random offset so the whole stack shifts up/down
            float availableSpace = (maxY - minY) - stackHeight;
            float offset = Random.Range(0f, availableSpace);

            // Starting Y (bottom of stack)
            float startY = minY + offset + objectHeight / 2f;

            float currentY = startY;

            for (int i = 0; i < count; i++)
            {
                Vector3 pos = new Vector3(7f, currentY, 0f);

                _applePool.TryGet(out GameObject apple);
                apple.GetComponent<Apple>()?.Init(pos);

                currentY += objectHeight + spacing;
            }
        }

        private void SpawnPipe()
        {
            _prev = Mathf.Clamp(_prev + Random.Range(-2f, 2f) * BattleSceneManager.Instance.Difficulty, -4f, 4f);
            float halfGap = Random.Range(2f, 3f) / ((BattleSceneManager.Instance.Difficulty - 1f) / 5f + 1f) / 2f;
            float offset = 1f / GameConstants.ppu * 4f; // 0.28125f

            //_prev = -4f;
            bool flag0 = _prev + halfGap < 3.5f;
            bool flag1 = _prev - halfGap > -3.5f;
            bool flag2 = BattleSceneManager.Instance.Difficulty > 1.5f;
            //flag2 = true;

            if ((flag0 ^ flag1) && flag2)
            {
                Vector3 bottom = Vector3.zero;
                Vector3 top = Vector3.zero;
                Vector3 capPos = Vector3.zero;
                float movingDist = 0f;
                bool faceUp = true;

                if (flag0)
                {
                    // moving top pipe
                    bottom = Vector3.zero;
                    top = Vector3.up * 12f;

                    movingDist = (Vector3.up * (_prev) / 1.5f).magnitude;
                    capPos = bottom + Vector3.up * offset;
                    faceUp = false;
                }

                if (flag1)
                {
                    // moving bottom pipe
                    top = Vector3.zero;

                    bottom = Vector3.up * -12f;

                    movingDist = (Vector3.up * (_prev) / 1.5f).magnitude;
                    capPos = top - Vector3.up * offset;
                }

                _movingPipePool.TryGet(out GameObject pipeObj);
                MovingPipe movingPipe = pipeObj.GetComponent<MovingPipe>();
                movingPipe.Init(bottom, top, capPos, movingDist, faceUp);
                movingPipe.transform.localPosition = new Vector3(7f, 0, 0f);
            }
            else
            {
                if (flag0)
                {
                    Vector3 bottom = Vector3.up * (_prev + halfGap);
                    Vector3 top = Vector3.up * 12f;
                    Vector3 capPos = bottom + Vector3.up * offset;

                    _idlePipePool.TryGet(out GameObject pipeTop);
                    Pipe pipe = pipeTop.GetComponent<Pipe>();
                    pipe.Init(bottom, top, capPos, false);
                    pipeTop.transform.localPosition = new Vector3(7f, 0, 0f);
                }

                if (flag1)
                {
                    Vector3 top = Vector3.up * (_prev - halfGap);
                    Vector3 bottom = Vector3.up * -12f;
                    Vector3 capPos = top - Vector3.up * offset;

                    _idlePipePool.TryGet(out GameObject pipeBottom);
                    Pipe pipe = pipeBottom.GetComponent<Pipe>();
                    pipe.Init(bottom, top, capPos);
                    pipeBottom.transform.localPosition = new Vector3(7f, 0, 0f);
                }
            }
        }
    }
}