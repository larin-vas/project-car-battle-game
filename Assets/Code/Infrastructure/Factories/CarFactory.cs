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
using Assets.Code.Transport.Car.CarMovement;
using Assets.Code.Transport.Car.CarHealth;
using Code.Map;
using Code.Services;

namespace Code.Infrastructure.Factories
{
    public class CarFactory : IFactory<CarConfig, CarController>
    {
        private readonly ConstantsService _constants;

        private readonly PhysicsService _physicsService;

        private readonly IMovableInput _input;

        private readonly IFactory<Transform, WheelConfig, WheelController> _wheelFactory;
        private readonly IFactory<CarView, GunConfig, GunController> _gunFactory;

        public CarFactory(
            ConstantsService constants,
            PhysicsService physicsService,
            IMovableInput input,
            IFactory<Transform, WheelConfig, WheelController> wheelFactory,
            IFactory<CarView, GunConfig, GunController> gunFactory)
        {
            _constants = constants;

            _physicsService = physicsService;

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

            view.Body.Construct(transformation, inertiaVector, collisionDamage);

            CarMovementModel movementModel = new CarMovementModel(
                transformation, inertiaVector, collisionDamage,
                config.AccelerationRate, config.MaxAcceleration,
                config.MassCenterPosition, config.Mass);

            CollisionTrigger collisionTrigger = new CollisionTrigger(_constants, view.Collider);

            CarMovementController movementController = new CarMovementController(_input, _physicsService, movementModel, view, collisionTrigger);

            CarHealthController healthController = CreateHealthController(config, collisionTrigger);

            WheelSystemController wheelSystem = CreateWheelSystem(config, view.transform, movementModel);

            WeaponSystemController weaponSystem = CreateWeaponSystem(config, view);

            return new CarController(movementController, healthController, wheelSystem, weaponSystem);
        }

        private CarHealthController CreateHealthController(CarConfig config, CollisionTrigger collisionTrigger)
        {
            CarHealthModel healthModel = new CarHealthModel(config.MaxHealthPoints);

            return new CarHealthController(healthModel, collisionTrigger);
        }

        private WheelSystemController CreateWheelSystem(CarConfig config, Transform wheelsParent, CarMovementModel movementModel)
        {
            List<WheelController> wheelControllers = new List<WheelController>();

            foreach (WheelConfig wheelConfig in config.Wheels)
            {
                wheelControllers.Add(_wheelFactory.Create(wheelsParent, wheelConfig));
            }

            return new WheelSystemController(_input, movementModel, wheelControllers);
        }

        private WeaponSystemController CreateWeaponSystem(CarConfig config, CarView carView)
        {
            List<IWeapon> gunControllers = new List<IWeapon>();

            foreach (GunConfig gunConfig in config.Guns)
            {
                gunControllers.Add(_gunFactory.Create(carView, gunConfig));
            }

            return new WeaponSystemController(gunControllers);
        }
    }
}