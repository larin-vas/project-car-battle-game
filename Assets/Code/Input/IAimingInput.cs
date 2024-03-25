using UnityEngine;

public interface IAimingInput
{
    public Vector2 AimTargetPoint { get; }

    public bool Shoot { get; }
}