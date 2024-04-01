using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Services
{
    public class ConstantsService
    {
        public int ProjectilePoolInitSize = 10;

        public float AngleToChangeDirection = 90f;

        public int MaxMapSeed = 100000;

        public int CollisionTriggerBufferSize = 100;

        public Vector2Int[] PathfinderAllowedDirections =
        {
            new Vector2Int(-1, -1),
            new Vector2Int(-1,  0),
            new Vector2Int(-1,  1),

            new Vector2Int( 0, -1),
            new Vector2Int( 0,  1),

            new Vector2Int( 1, -1),
            new Vector2Int( 1,  0),
            new Vector2Int( 1,  1)
        };
    }
}