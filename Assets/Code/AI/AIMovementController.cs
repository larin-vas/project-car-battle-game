using Code.AI.Pathfinding;
using Code.Car;
using Code.Common.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.AI
{
    public class AIMovementController
    {
        private readonly IPathfinder _pathfinder;

        private readonly IReadOnlyMovable _targetObject;

        private readonly AIMovableInput _input;

        public AIMovementController(IPathfinder pathfinder, IReadOnlyMovable targetObject) : this(pathfinder, targetObject, new AIMovableInput())
        { }

        public AIMovementController(IPathfinder pathfinder, IReadOnlyMovable targetObject, AIMovableInput movableInput)
        {
            _pathfinder = pathfinder;

            _targetObject = targetObject;

            _input = movableInput;
        }

        public void UpdateObjectInput(ControllableTransport controlledObject)
        {
            if (_targetObject == null || controlledObject == null)
                return;

            controlledObject.SetInput(_input);

            if (IsCloseEnoughToStop(controlledObject))
            {
                _input.StopMovement();

                _input.AimTargetPoint = _targetObject.GetPosition();
                _input.Shoot = true;

                return;
            }

            Vector2Int targetPosition = Vector2Int.RoundToInt(_targetObject.GetPosition());

            Vector2Int controlledObjectPosition = Vector2Int.RoundToInt(controlledObject.GetPosition());

            IReadOnlyList<Vector2Int> path = _pathfinder.FindPath(controlledObjectPosition, targetPosition);

            if (path != null && path.Count > 0)
            {
                Vector2 moveDirection = (path[1] - controlledObject.GetPosition()).normalized;

                // ¬ычисл€ем угол между текущим поворотом и целевым направлением
                float angleDifference = CalculateAngleDifference(controlledObject, moveDirection);

                _input.Rotation = CalculateRotation(angleDifference, angleSign);

                _input.Movement = CalculateMovement(angleDifference);

                _input.Brake = false;
                _input.Handbrake = false;

                _input.AimTargetPoint = _targetObject.GetPosition();
                _input.Shoot = false;
            }
        }

        private bool IsCloseEnoughToStop(IReadOnlyMovable controlledObject)
        {
            Vector2 directionVector = _targetObject.GetPosition() - controlledObject.GetPosition();

            return directionVector.magnitude < 5f;
        }

        private float CalculateAngleDifference(IReadOnlyMovable controlledObject, Vector2 moveDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

            // ¬ычисл€ем знак угла
            Vector3 cross = Vector3.Cross(Vector3.forward, moveDirection);
            float angleSign = Mathf.Sign(Vector3.Dot(cross, controlledObject.GetRotation() * Vector2.up));

            return angleSign * Quaternion.Angle(controlledObject.GetRotation(), targetRotation);
        }

        private float CalculateRotation(float angleDifference, float sign)
        {
            return (angleDifference < 90f) ? sign : -sign;
        }

        private float CalculateMovement(float angleDifference)
        {
            return (angleDifference < 90f) ? 1f : -1f;
        }
    }
}