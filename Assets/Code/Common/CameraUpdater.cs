using Code.Common;
using UnityEngine;

public class CameraUpdater : MonoBehaviour
{
    private Transformation _transformation;

    private Observable<float> _height;

    public void Construct(Transformation transformation, Observable<float> height)
    {
        _transformation = transformation;
        _height = height;

        _transformation.Position.OnChanged += SetPosition;
        _transformation.Rotation.OnChanged += SetRotation;

        _height.OnChanged += SetHeight;

        SetPosition(_transformation.Position.Value);
        SetRotation(_transformation.Rotation.Value);

        SetHeight(_height.Value);
    }

    private void OnEnable()
    {
        if (_transformation == null || _height == null)
            return;

        SetPosition(_transformation.Position.Value);
        SetRotation(_transformation.Rotation.Value);

        SetHeight(_height.Value);
    }

    private void OnDestroy()
    {
        if (_transformation == null || _height == null)
            return;

        _transformation.Position.OnChanged -= SetPosition;
        _transformation.Rotation.OnChanged -= SetRotation;

        _height.OnChanged -= SetHeight;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;

        SetHeight(_height.Value);
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void SetHeight(float height)
    {
        Vector3 newPosition = transform.position;
        newPosition.z = height;
        transform.position = newPosition;
    }
}
