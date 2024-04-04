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
    [field: SerializeField]
    public CarConfig PlayerCarData { get; private set; }

    [field: SerializeField]
    public CarConfig EnemyCarData { get; private set; }

    [field: SerializeField]
    public MapConfig MapData { get; private set; }

    [field: SerializeField]
    public LevelConfig LevelData { get; private set; }

    [field: SerializeField]
    public EnemyGroupConfig EnemyGroupData { get; private set; }

    [field: SerializeField]
    public CameraConfig CameraData { get; private set; }

    [field: SerializeField]
    public PlayerInput PlayerInput { get; private set; }

    //[field: SerializeField]
    //public PlayerInputConfig InputData { get; private set; }

    public override void InstallBindings()
    {
        //Container.Bind<PlayerInput>().FromInstance(PlayerInput).AsSingle();

        //Container.BindInterfacesAndSelfTo<PlayerInputConfig>().FromInstance(InputData).AsSingle();

        Container.BindInterfacesAndSelfTo<MapConfig>().FromInstance(MapData).AsSingle();

        Container.BindInterfacesAndSelfTo<LevelConfig>().FromInstance(LevelData).AsSingle();

        Container.BindInterfacesAndSelfTo<CameraConfig>().FromInstance(CameraData).AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyGroupConfig>().FromInstance(EnemyGroupData).AsSingle();

        Container.Bind<CarConfig>().WithId("Player").FromInstance(PlayerCarData);

        Container.Bind<CarConfig>().WithId("Enemy").FromInstance(EnemyCarData);

        Container.BindInterfacesAndSelfTo<MainPlayerInput>().AsSingle();

        Container.BindInterfacesAndSelfTo<CameraFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<WheelFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<GunFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<CarFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<EnemyGroupFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<TileModelFactory>().AsSingle();

        Container.Bind<MapController>().FromFactory<MapFactory>().AsSingle();

        Container.BindInterfacesAndSelfTo<AStarPathfinder>().AsSingle();

        Container.BindInterfacesAndSelfTo<LevelController>().AsSingle();

        Container.Resolve<LevelController>().Start();
    }

}
