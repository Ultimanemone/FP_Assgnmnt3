using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public class PooledAudioObj : PooledObj
    {
        private AudioSource _as;

        private void Awake()
        {
            _as = GetComponent<AudioSource>();
        }

        public void PlayClip(AudioClip clip, float volume)
        {
            _as.clip = clip;
            _as.volume = volume;
            _as.Play();
            StartCoroutine(ReturnCR(clip.length + 0.1f));
        }

        private IEnumerator ReturnCR(float delay)
        {
            yield return new WaitForSeconds(delay);
            _pool.Return(gameObject);
            yield break;
        }
    }
}