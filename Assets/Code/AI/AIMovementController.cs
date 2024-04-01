using Code.AI.Pathfinding;
using Code.Car;
using Code.Common.Interfaces;
using Code.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Code.AI
{
    public class AIMovementController
    {
        private readonly EnemyGroupModel _model;

        private readonly IPathfinder _pathfinder;

        private readonly IReadOnlyMovable _targetObject;

        private readonly ConstantsService _constants;

        private readonly AIMovableInput _input;

        public AIMovementController(
            EnemyGroupModel model,
            IPathfinder pathfinder,
            IReadOnlyMovable targetObject,
            ConstantsService constants) :
            this(model, pathfinder, targetObject, constants, new AIMovableInput())
        { }

        public AIMovementController(
            EnemyGroupModel model,
            IPathfinder pathfinder,
            IReadOnlyMovable targetObject,
            ConstantsService constants,
            AIMovableInput movableInput)
        {
            _model = model;

            _pathfinder = pathfinder;

            _targetObject = targetObject;

            _constants = constants;

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

            if (path != null && path.Count >= _model.PathSegmentIndex)
            {
                Vector2 moveDirection = (path[_model.PathSegmentIndex] - controlledObject.GetPosition()).normalized;

                // Calculate the angle between the current turn and the target direction
                float angleDifference = CalculateAngleDifference(controlledObject, moveDirection);

                _input.Rotation = CalculateRotation(angleDifference);

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

            return directionVector.magnitude < _model.AttackRange;
        }

        private float CalculateAngleDifference(IReadOnlyMovable controlledObject, Vector2 moveDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);

            Vector3 cross = Vector3.Cross(Vector3.forward, moveDirection);
            float angleSign = Mathf.Sign(Vector3.Dot(cross, controlledObject.GetRotation() * Vector2.up));

            return angleSign * Quaternion.Angle(controlledObject.GetRotation(), targetRotation);
        }

        private float CalculateRotation(float angleDifference)
        {
            float angleSign = Mathf.Sign(angleDifference);
            return (Mathf.Abs(angleDifference) < _constants.AngleToChangeDirection) ? angleSign : -angleSign;
        }

        private float CalculateMovement(float angleDifference)
        {
            return (angleDifference < _constants.AngleToChangeDirection) ? 1f : -1f;
        }
    }
}