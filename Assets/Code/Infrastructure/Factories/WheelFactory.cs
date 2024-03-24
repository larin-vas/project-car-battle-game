using Code.Common;
using Code.Wheel;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class WheelFactory : IFactory<WheelConfig, WheelController>, IFactory<Transform, WheelConfig, WheelController>
    {
        private readonly IMovableInput _input;

        public WheelFactory(IMovableInput input)
        {
            _input = input;
        }

        public WheelController Create(WheelConfig config)
        {
            return Create(null, config);
        }

        public WheelController Create(Transform parentCarTransform, WheelConfig config)
        {
            WheelView view = Object.Instantiate(config.WheelView, parentCarTransform);

            Transformation transformation = new Transformation();

            WheelModel model = new WheelModel(
                transformation, config.LocalPosition, config.Alignment,
                config.MinRotationAngle, config.MaxRotationAngle, config.RotationSpeed,
                config.IsDriveWheel, config.IsLockedOnHandbrake);

            view.Transformable.Construct(transformation);

            WheelController wheel = new WheelController(_input, model, view);

            return wheel;
        }
    }
}