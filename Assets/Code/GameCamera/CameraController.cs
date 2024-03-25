using Code.Common.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.GameCamera
{
    public class CameraController : IMovable, IActivatable, ITickable
    {
        private IReadOnlyMovable _trackedObject;

        private readonly CameraModel _model;
        private readonly CameraView _view;

        public CameraController(
            IReadOnlyMovable trackedObject,
            CameraModel model, CameraView view)
        {
            _trackedObject = trackedObject;

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
            _model.Transformation.Position.Value = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _model.Transformation.Rotation.Value = rotation;
        }

        public Camera GetCamera()
        {
            return _view.Camera;
        }

        public void UpdateTrackedObject(IReadOnlyMovable trackedObject)
        {
            _trackedObject = trackedObject;
        }

        public void Tick()
        {
            Vector3 newPosition = _trackedObject.GetPosition();
            SetPosition(newPosition);
        }
    }
}