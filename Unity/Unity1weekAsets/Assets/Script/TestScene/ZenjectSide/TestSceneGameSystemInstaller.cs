using UnityEngine;
using Zenject;
using My1WeekGameSystems_ver1;

public class TestSceneGameSystemInstaller : MonoInstaller {
    public override void InstallBindings() {
        //InputManager
        Container
            .Bind<I_InputUpdatable>()
            .To<InputManager>()
            .AsSingle();

        Container
            .Bind<I_InputDataAddable>()
            .To<InputManager>()
            .AsSingle();

        //ObjectUpdater
        Container
            .Bind<SceneObjectUpdateManager>()
            .To<TestSceneObjectUpdater>()
            .AsSingle();

        //GameManager
        Container
            .Bind<GameManager>()
            .To<TestSceneGameManager>()
            .AsSingle();

        Container
            .Bind<I_SceneLoadAlertable>()
            .To<TestSceneGameManager>()
            .AsSingle();

        Container
            .Bind<I_GameModeChangeAlertable>()
            .To<TestSceneGameManager>()
            .AsSingle();
    }
}