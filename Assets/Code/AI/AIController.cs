using Code.AI.Pathfinding;
using Code.Car;
using Code.Common.Interfaces;
using Code.Infrastructure.Pools;
using Code.Infrastructure.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Code.AI
{
    public class AIController : ITickable
    {
        private readonly IPathfinder _pathfinder;

        private readonly ControllableTransport _controlledObject;

        private readonly IMovable _targetObject;

        private readonly AIMovableInput _input;

        private readonly IPool<ControllableTransport> _pool;

        public AIController(IPathfinder pathfinder, ControllableTransport controlledObject, IMovable targetObject)
        {
            _controlledObject = controlledObject;

            _targetObject = targetObject;

            _pathfinder = pathfinder;

            _input = new AIMovableInput();

            _controlledObject.SetInput(_input);
        }

        public void Tick()
        {
            if (_controlledObject == null || _targetObject == null)
                return;

            Vector2Int controlledObjectPosition = Vector2Int.RoundToInt(_controlledObject.GetPosition() + (Vector2)(_controlledObject.GetRotation() * Vector2.up * 0.3f));
            Vector2Int targetPosition = Vector2Int.FloorToInt(_targetObject.GetPosition());

            IReadOnlyList<Vector2Int> path = _pathfinder.FindPath(controlledObjectPosition, targetPosition);

            if (path != null && path.Count > 5)
            {
                Vector2 targetDirection = (path[1] - _controlledObject.GetPosition()).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, targetDirection);

                // ¬ычисл€ем угол между текущим поворотом и целевым направлением
                float angleDifference = Quaternion.Angle(_controlledObject.GetRotation(), targetRotation);

                // ¬ычисл€ем знак угла
                Vector3 cross = Vector3.Cross(Vector3.forward, targetDirection);
                float sign = Mathf.Sign(Vector3.Dot(cross, _controlledObject.GetRotation() * Vector2.up));

                _input.Rotation = (angleDifference < 90f) ? sign : -sign;

                _input.Movement = (angleDifference < 90f) ? 1f : -1f;

                _input.Brake = false;
                _input.Handbrake = false;

                _input.Shoot = false;
                Vector2 direction = (_targetObject.GetPosition() - _controlledObject.GetPosition()).normalized;
                _input.Direction = direction;
            }
            else
            {
                _input.Rotation = 0f;
                _input.Movement = 0f;
                _input.Brake = true;
                _input.Handbrake = true;

                _input.Shoot = true;
                Vector2 direction = (_targetObject.GetPosition() - _controlledObject.GetPosition()).normalized;
                _input.Direction = direction;
            }
            _controlledObject.Tick();
        }
    }
}