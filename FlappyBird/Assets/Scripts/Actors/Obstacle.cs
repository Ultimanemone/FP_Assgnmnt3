using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected void FixedUpdate()
    {
        OnFixedUpdate();
    }

    protected virtual void OnFixedUpdate() { }
}
