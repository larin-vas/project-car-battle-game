using UnityEngine;

namespace Code.Map
{
    public interface IWorldProperties
    {
        public float GetElasticityByPosition(Vector2Int position);

        public float GetFrictionByPosition(Vector2Int position);

        public float GetAirResistanceByPosition(Vector2Int position);
    }
}
