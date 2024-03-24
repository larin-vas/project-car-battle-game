using Code.Combat.Gun;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Combat
{
    public class WeaponSystemController : ITickable
    {
        private readonly ICollection<GunController> _guns;

        public WeaponSystemController(ICollection<GunController> guns)
        {
            _guns = guns;
        }

        public void Enable()
        {
            foreach (GunController gun in _guns)
            {
                gun.Enable();
            }
        }

        public void Disable()
        {
            foreach (GunController gun in _guns)
            {
                gun.Disable();
            }
        }

        public void SetPosition(Vector2 position)
        {
            foreach (GunController gun in _guns)
            {
                gun.SetPosition(position);
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            foreach (GunController gun in _guns)
            {
                gun.SetRotation(rotation);
            }
        }

        public void SetInput(IInput input)
        {
            foreach (GunController gun in _guns)
            {
                gun.SetInput(input);
            }
        }

        public void Tick()
        {
            foreach (GunController gun in _guns)
            {
                gun.Tick();
            }
        }
    }
}
