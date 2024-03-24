using Code.Common.Interfaces;
using System;
using UnityEngine;
using Zenject;

namespace Code.GameCamera
{
    public class CameraController : IMovable, ITickable
    {
        private IReadOnlyMovable _trackedObject;

        private readonly CameraModel _cameraModel;
        private readonly CameraView _cameraView;

        public CameraController(
            IReadOnlyMovable trackedObject,
            CameraModel cameraModel, CameraView cameraView)
        {
            _trackedObject = trackedObject;
            _cameraModel = cameraModel;
            _cameraView = cameraView;
        }

        public void Enable()
        {
            _cameraView.Activator.Enable();
        }

        public void Disable()
        {
            _cameraView.Activator.Disable();
        }

        public Vector2 GetPosition()
        {
            return _cameraModel.Transformation.Position.Value;
        }

        public Quaternion GetRotation()
        {
            return _cameraModel.Transformation.Rotation.Value;
        }

        public void SetPosition(Vector2 position)
        {
            _cameraModel.Transformation.Position.Value = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            _cameraModel.Transformation.Rotation.Value = rotation;
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