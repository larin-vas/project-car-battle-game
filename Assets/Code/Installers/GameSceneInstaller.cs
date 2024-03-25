using Code.AI.Pathfinding;
using Code.Infrastructure.Factories;
using Code.Infrastructure.ScriptableObjects;
using Code.Level;
using Code.Map;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField]
    public CarConfig CarData;

    [SerializeField]
    public CarConfig EnemyCarData;

    [SerializeField]
    public MapConfig MapData;

    [SerializeField]
    public CameraConfig CameraData;

    [SerializeField]
    public Vector2 PlayerPosition;

    [SerializeField]
    public Vector2 AiPosition;

    [field: SerializeField]
    public PlayerInput PlayerInput { get; set; }

    [SerializeField]
    public PlayerInputConfig InputData;

    [field: SerializeField]
    public Camera MainCamera { get; set; }

    public override void InstallBindings()
    {
        Container.Bind<PlayerInput>().FromInstance(PlayerInput).AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerInputConfig>().FromInstance(InputData).AsSingle();

        Container.BindInterfacesAndSelfTo<MapConfig>().FromInstance(MapData).AsSingle();

        Container.BindInterfacesAndSelfTo<CameraConfig>().FromInstance(CameraData).AsSingle();

        Container.Bind<CarConfig>().WithId("Player").FromInstance(CarData);

        Container.Bind<CarConfig>().WithId("Enemy").FromInstance(EnemyCarData);

        Container.BindInterfacesAndSelfTo<MainPlayerInput>().AsSingle();

        Container.BindInterfacesAndSelfTo<CameraFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<WheelFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<GunFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<CarFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<TileModelFactory>().AsSingle();

        Container.Bind<MapController>().FromFactory<MapFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<AStarPathfinder>().AsSingle();

        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();

        Container.Resolve<LevelController>().Start();
    }

}
