using Code.Common;
using Code.Common.Interfaces;
using Code.GameCamera;
using Code.Infrastructure.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class CameraFactory : IFactory<IMovable, CameraController>
    {
        private readonly CameraConfig _config;

        public CameraFactory(CameraConfig config)
        {
            _config = config;
        }

        public CameraController Create(IMovable trackedObject)
        {
            if (_config == null)
                throw new System.NullReferenceException();

            CameraView view = Object.Instantiate(_config.CameraView);

            Transformation transformation = new Transformation();

            CameraModel model = new CameraModel(transformation, _config.CameraHeight);

            view.CameraUpdater.Construct(transformation, model.CameraHeight);

            CameraController gameCamera = new CameraController(trackedObject, model, view);

            return gameCamera;
        }
    }
}