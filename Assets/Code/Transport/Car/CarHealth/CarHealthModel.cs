using System;

namespace Code.Transport.Car.CarHealth
{
    public class CarHealthModel
    {
        public float CurrentHealthPoints
        {
            get => _currentHealthPoints;
            set
            {
                if (value >= 0 && value <= MaxHealthPoints)
                    _currentHealthPoints = value;
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        public float MaxHealthPoints { get; }

        private float _currentHealthPoints;


        public CarHealthModel(float maxHealthPoints) : this(maxHealthPoints, maxHealthPoints)
        { }

        public CarHealthModel(float maxHealthPoints, float currentHealthPoints)
        {
            if (maxHealthPoints < 0)
                throw new ArgumentOutOfRangeException(nameof(maxHealthPoints));

            MaxHealthPoints = maxHealthPoints;
            CurrentHealthPoints = currentHealthPoints;
        }
    }
}