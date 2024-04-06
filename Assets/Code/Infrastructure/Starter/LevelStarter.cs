using Code.Level;
using UnityEngine;
using Zenject;

namespace Assets.Code.Infrastructure.Starter
{
    public class LevelStarter : MonoBehaviour
    {
        private LevelController _levelController;

        [Inject]
        public void Construct(LevelController levelController)
        {
            _levelController = levelController;
        }

        void Start()
        {
            _levelController.Start();
        }
    }
}