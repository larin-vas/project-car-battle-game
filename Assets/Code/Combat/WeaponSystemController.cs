using System.Collections.Generic;
using UnityEngine;

namespace Code.Combat
{
    public class WeaponSystemController : IWeaponSystem
    {
        private readonly ICollection<IWeapon> _guns;

        public WeaponSystemController(ICollection<IWeapon> guns)
        {
            _guns = guns;
        }

        public void Enable()
        {
            foreach (IWeapon gun in _guns)
            {
                gun.Enable();
            }
        }

        public void Disable()
        {
            foreach (IWeapon gun in _guns)
            {
                gun.Disable();
            }
        }

        public void SetPosition(Vector2 position)
        {
            foreach (IWeapon gun in _guns)
            {
                gun.SetPosition(position);
            }
        }

        public void SetRotation(Quaternion rotation)
        {
            foreach (IWeapon gun in _guns)
            {
                gun.SetRotation(rotation);
            }
        }

        public void SetInput(IAimingInput input)
        {
            foreach (IWeapon gun in _guns)
            {
                gun.SetInput(input);
            }
        }

        public void Tick()
        {
            foreach (IWeapon gun in _guns)
            {
                gun.Tick();
            }
        }
    }
}
