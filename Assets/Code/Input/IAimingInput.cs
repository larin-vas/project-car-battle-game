using UnityEngine;

public interface IAimingInput
{
    public Vector2 AimDirection { get; }

    public bool Shoot { get; }
}