using Code.Common.Interfaces;
using Code.Physics.Force;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.Wheel
{
    public interface IWheelSystem : ISetOnlyMovable, IActivatable, ITickable
    {
        public IEnumerable<PhysicForce> GetWheelsForces();

        public void SetInput(IMovableInput input);

        public Vector2 CalculateRotationCenter();
    }
}
