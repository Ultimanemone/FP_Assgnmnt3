using System.Collections.Generic;
using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public enum SFXID
    {
        FLAP = 0,
        APPLE = 1,

        LOSE = 2,
        START = 3,
        TICK = 4,
        PAUSE = 5,
        UNPAUSE = 6,
        BUTTON = 7
    }

    public class AudioPlayer : MonoBehaviour
    {
        private AudioSource _musicSource;
        private ObjPool _sfxPool;
        [SerializeField] private List<AudioClip> _sfxList;
        [SerializeField] private GameObject _pooledAudioObj;
        public static AudioPlayer Instance;

        private AudioPlayer() { }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            _musicSource = GetComponent<AudioSource>();
            _sfxPool = new ObjPool(_pooledAudioObj, 20, transform);
        }

        public void PlayClipID(int id, float volume = 1f)
        {
            _sfxPool.TryGet(out GameObject source);
            source.GetComponent<PooledAudioObj>()?.PlayClip(_sfxList[id], volume);
        }

        public void ToggleMusic()
        {
            if (!_musicSource.isPlaying) _musicSource.Play();
            else _musicSource.Pause();
        }

        public void ToggleMusic(bool toggle)
        {
            if (!_musicSource.isPlaying && toggle) _musicSource.Play();
            else if (_musicSource.isPlaying && !toggle) _musicSource.Pause();
        }
    }
}