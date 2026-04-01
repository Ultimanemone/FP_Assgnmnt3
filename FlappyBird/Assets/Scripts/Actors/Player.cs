using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class Player : MovingObj
    {
        private enum AnimState
        {
            IDLE,
            FLAP,
            FLAP_LOOP
        }

        private Vector3 _speed;
        private Animator _animator;
        private AnimState _currentAnimState;
        public static Player instance { get; private set; }
        [SerializeField] private ParticleSystem _ps;

        private Player() { }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            _speed = Vector2.zero;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            Animate(AnimState.FLAP_LOOP);
        }

        protected override void FixedUpdate()
        {
            if (BattleSceneManager.Instance.State == BattleSceneState.Playing)
            {
                if (_currentAnimState == AnimState.FLAP_LOOP) Animate(AnimState.IDLE);

                _speed.y = Mathf.Max(_speed.y - 7f * Time.deltaTime, -5f);
                Vector3 newPos = transform.localPosition;
                newPos.y = Mathf.Clamp(newPos.y + _speed.y * Time.deltaTime, -3.75f, 3.75f);
                transform.localPosition = newPos;
                if (newPos.y < -3.74f)
                    transform.localEulerAngles = Vector3.forward * (newPos - transform.localPosition).y * 12f;
                else
                    transform.localEulerAngles = Vector3.forward * _speed.y * 12f;

                _ps.transform.position = transform.position;
            }
        }

        public void Flap()
        {
            _speed.y = 3f;
            Animate(AnimState.FLAP);
            _ps.Play();
            AudioPlayer.Instance.PlayClipID((int)SFXID.FLAP);
        }

        private void Animate(AnimState state = AnimState.IDLE)
        {
            _currentAnimState = state;
            switch (state)
            {
                default:
                    _animator.Play("idle");
                    break;
                case AnimState.FLAP:
                    _animator.Play("flap");
                    break;
                case AnimState.FLAP_LOOP:
                    _animator.Play("flapLoop");
                    break;
            }
        }
    }
}
