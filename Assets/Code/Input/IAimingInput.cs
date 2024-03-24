using UnityEngine;

public interface IAimingInput
{
    public Vector2 Direction { get; }

    public bool Shoot { get; }
}