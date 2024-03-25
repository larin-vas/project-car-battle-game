using Code.Combat.Gun;
using Code.Combat.Projectile;
using Code.Common;
using Code.Infrastructure.Pools;
using Code.Infrastructure.ScriptableObjects;
using Code.Transport.Car;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Factories
{
    public class GunFactory : IFactory<CarView, GunConfig, GunController>
    {
        private readonly IAimingInput _input;

        //private readonly IFactory<ProjectileConfig, ProjectileController> _projectileFactory;

        public GunFactory(IAimingInput input)
        {
            _input = input;
            //_projectileFactory = projectileFactory;
        }

        public GunController Create(CarView parentCarView, GunConfig config)
        {
            if (config == null)
                throw new System.NullReferenceException();

            GunView view = Object.Instantiate(config.GunView, parentCarView.transform);

            Transformation transformation = new Transformation();

            GunModel model = new GunModel(transformation, config.LocalPosition, config.RotationSpeed, config.ReloadTime);

            view.PhysicObjectUpdater.Construct(transformation);

            ProjectileFactory projectileFactory = new ProjectileFactory(parentCarView.Collider);

            ObjectPool<ProjectileConfig, ProjectileController> projectilePool =
                new ObjectPool<ProjectileConfig, ProjectileController>(projectileFactory, config.ProjectileConfig, 20);

            GunController gun = new GunController(_input, model, view, projectilePool);

            return gun;
        }
    }
}