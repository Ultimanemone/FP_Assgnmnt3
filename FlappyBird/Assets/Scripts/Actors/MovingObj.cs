using Flappy_Assgnmt3.Core;
using UnityEngine;

namespace Flappy_Assgnmt3.Actors
{
    public class MovingObj : MonoBehaviour
    {
        protected virtual void FixedUpdate()
        {
            OnFixedUpdate();
        }

        protected virtual void OnFixedUpdate() { }
    }
}
