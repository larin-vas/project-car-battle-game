using Code.Combat;
using Code.Combat.Gun;
using Code.Common;
using Code.Physics;
using Code.Transport.Car;
using Code.Wheel;
using Code.Infrastructure.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class CarFactory : IFactory<CarConfig, CarController>
    {
        private readonly IMovableInput _input;

        private readonly IFactory<Transform, WheelConfig, WheelController> _wheelFactory;
        private readonly IFactory<CarView, GunConfig, GunController> _gunFactory;

        public CarFactory(
            IMovableInput input,
            IFactory<Transform, WheelConfig, WheelController> wheelFactory,
            IFactory<CarView, GunConfig, GunController> gunFactory)
        {
            _input = input;

            _wheelFactory = wheelFactory;
            _gunFactory = gunFactory;
        }

        public CarController Create(CarConfig config)
        {
            if (config == null)
                throw new System.NullReferenceException();

            CarView view = Object.Instantiate(config.CarView);

            Transformation transformation = new Transformation();

            Observable<Vector2> inertiaVector = new Observable<Vector2>(Vector2.zero);

            Observable<float> collisionDamage = new Observable<float>(config.CollisionDamage);

            CarModel model = new CarModel(
                transformation, inertiaVector, collisionDamage,
                config.MaxHealthPoints,
                config.AccelerationRate, config.MaxAcceleration,
                config.MassCenterPosition, config.Mass);

            view.Body.Construct(transformation, inertiaVector, collisionDamage);

            List<WheelController> wheelControllers = new List<WheelController>();

            foreach (WheelConfig wheelConfig in config.Wheels)
            {
                wheelControllers.Add(_wheelFactory.Create(view.transform, wheelConfig));
            }

            List<IWeapon> gunControllers = new List<IWeapon>();

            foreach (GunConfig gunConfig in config.Guns)
            {
                gunControllers.Add(_gunFactory.Create(view, gunConfig));
            }

            WheelSystemController wheelSystem = new WheelSystemController(_input, model, wheelControllers);

            WeaponSystemController weaponSystem = new WeaponSystemController(gunControllers);

            CollisionTrigger collisionTrigger = new CollisionTrigger(view.Collider);

            CarController car = new CarController(_input, model, view, wheelSystem, weaponSystem, collisionTrigger);

            return car;
        }
    }
}