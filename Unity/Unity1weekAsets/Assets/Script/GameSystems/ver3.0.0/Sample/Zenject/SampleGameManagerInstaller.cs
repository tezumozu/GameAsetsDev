using UnityEngine;
using Zenject;

using My1WeekGameSystems_ver3;

public class SampleGameManagerInstaller : MonoInstaller{
    public override void InstallBindings(){
        var gameManager = new SampleGameManager();

        //GameManager
        Container
            .Bind<I_SceneLoadNoticable>()
            .To<SampleGameManager>()
            .FromInstance(gameManager);	

        Container
            .Bind<I_GameStateUpdatable<E_SampleSceneState>>()
            .To<SampleGameManager>()
            .FromInstance(gameManager);

        Container
            .Bind<GameManager<E_SampleSceneState>>()
            .To<SampleGameManager>()
            .FromInstance(gameManager);


        //ObjectUpdater
        Container
            .Bind<I_ObjectUpdatable>()
            .To<SampleObjectUpdater>()
            .AsSingle();
        
    }
}