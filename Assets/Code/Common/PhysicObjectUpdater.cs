using Code.Common;
using Code.Physics;
using UnityEngine;

public class PhysicObjectUpdater : MonoBehaviour, IPhysicObject
{
    public Vector2 Force => _forceVector.Value;

    public float CollisionDamage => _collisionDamage.Value;

    public float ElasticityRate => _elasticityRate.Value;

    public Collider2D ParentCollider => _parentCollider;

    private Transformation _transformation;

    private Observable<Vector2> _forceVector;
    private Observable<float> _collisionDamage;
    private Observable<float> _elasticityRate;
    private Collider2D _parentCollider;

    public void Construct(Transformation transformation)
    {
        Construct(transformation, new Observable<Vector2>(Vector2.zero));
    }

    public void Construct(Transformation transformation, Observable<Vector2> forceVector)
    {
        Construct(transformation, forceVector, new Observable<float>(0f));
    }

    public void Construct(Transformation transformation, Observable<Vector2> forceVector, Observable<float> collisionDamage)
    {
        Construct(transformation, forceVector, collisionDamage, new Observable<float>(0f));
    }

    public void Construct(
        Transformation transformation, Observable<Vector2> forceVector,
        Observable<float> collisionDamage, Observable<float> elasticityRate)
    {
        Construct(transformation, forceVector, collisionDamage, elasticityRate, null);
    }

    public void Construct(
        Transformation transformation, Observable<Vector2> forceVector,
        Observable<float> collisionDamage, Observable<float> elasticityRate,
        Collider2D parentCollider)
    {
        _transformation = transformation;
        _forceVector = forceVector;
        _collisionDamage = collisionDamage;
        _elasticityRate = elasticityRate;
        _parentCollider = parentCollider;

        _transformation.Position.OnChanged += SetPosition;
        _transformation.Rotation.OnChanged += SetRotation;

        SetPosition(_transformation.Position.Value);
        SetRotation(_transformation.Rotation.Value);
    }

    private void OnEnable()
    {
        if (_transformation == null)
            return;

        SetPosition(_transformation.Position.Value);
        SetRotation(_transformation.Rotation.Value);
    }

    private void OnDestroy()
    {
        if (_transformation == null)
            return;

        _transformation.Position.OnChanged -= SetPosition;
        _transformation.Rotation.OnChanged -= SetRotation;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void SetRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}
