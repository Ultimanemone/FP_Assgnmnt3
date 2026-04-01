using System.Collections.Generic;
using UnityEngine;

namespace Flappy_Assgnmt3.Core
{
    public class AudioPlayerWrapper : MonoBehaviour
    {
        public void PlayClipID(int id)
        {
            AudioPlayer.Instance.PlayClipID(id);
        }

        public void ToggleMusic()
        {
            AudioPlayer.Instance.ToggleMusic();
        }
    }
}