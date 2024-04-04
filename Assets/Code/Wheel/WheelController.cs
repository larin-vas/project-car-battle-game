using Code.Common.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Wheel
{
    public class WheelController : IMovable, ITickable
    {
        private IMovableInput _input;

        private readonly WheelModel _model;
        private readonly WheelView _view;

        public WheelController(IMovableInput input, WheelModel model, WheelView view)
        {
            _input = input;
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.Activator.Enable();
        }

        public void Disable()
        {
            _view.Activator.Disable();
        }

        public WheelAlignment GetAlignment()
        {
            return _model.Alignment;
        }

        public bool IsLockedOnHandbrake()
        {
            return _model.IsLockedOnHandbrake;
        }

        public bool IsSteerableWheel()
        {
            return
                (Mathf.Abs(_model.RotationSpeed) > 0) &&
                ((Mathf.Abs(_model.MinRotationAngle) > 0) || (Mathf.Abs(_model.MaxRotationAngle) > 0));
        }

        public bool IsDriveWheel()
        {
            return _model.IsDriveWheel;
        }

        public Vector2 GetPosition()
        {
            return _model.Transformation.Position.Value;
        }

        public Quaternion GetRotation()
        {
            return _model.Transformation.Rotation.Value;
        }

        public void SetPosition(Vector2 position)
        {
            Quaternion rotationWithoutLocal = _model.Transformation.Rotation.Value * Quaternion.Inverse(_model.LocalRotation);
            Vector2 absolutePosition = (Vector3)position + rotationWithoutLocal * _model.LocalPosition;
            SetAbsolutePosition(absolutePosition);
        }

        public void SetRotation(Quaternion rotation)
        {
            SetAbsoluteRotation(rotation * _model.LocalRotation);
        }

        public void SetInput(IMovableInput input)
        {
            _input = input;
        }

        public void Tick()
        {
            if (IsSteerableWheel())
            {
                Quaternion currentLocalRotation = Quaternion.Slerp(_model.LocalRotation, GetRotationByInput(), _model.RotationSpeed * Time.deltaTime);
                _model.LocalRotation = currentLocalRotation;
            }
        }

        private void SetAbsolutePosition(Vector2 position)
        {
            _model.Transformation.Position.Value = position;
        }

        private void SetAbsoluteRotation(Quaternion rotation)
        {
            _model.Transformation.Rotation.Value = rotation;
        }

        private Quaternion GetRotationByInput()
        {
            return
                (IsWheelInner() || _model.Alignment == WheelAlignment.Center) ?
                Quaternion.Euler(0, 0, -_input.Rotation * _model.MaxRotationAngle) :
                Quaternion.Euler(0, 0, -_input.Rotation * _model.MinRotationAngle);
        }

        private bool IsWheelInner()
        {
            return
                ((_model.Alignment == WheelAlignment.Left) && (_input.Rotation < 0)) ||
                ((_model.Alignment == WheelAlignment.Right) && (_input.Rotation > 0));
        }
    }
}