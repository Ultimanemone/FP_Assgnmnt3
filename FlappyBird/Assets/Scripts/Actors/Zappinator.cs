using UnityEngine;
using UnityEngine.VFX;

public class Zappinator : Obstacle
{
    [SerializeField] private VisualEffect _effect;


    private void Awake()
    {
        _effect.SetInt("size", (int)Mathf.Round(10f * (_effect.GetVector3("start") + _effect.GetVector3("end")).magnitude));
    }

    protected override void OnFixedUpdate()
    {
        if (_effect != null && !Utils.IsWithinBounds(_effect.GetVector3("end")) && !Utils.IsWithinBounds(_effect.GetVector3("start")))
        {
            gameObject.SetActive(false);
            // ObjPool.Return(self);
        }
    }
}
