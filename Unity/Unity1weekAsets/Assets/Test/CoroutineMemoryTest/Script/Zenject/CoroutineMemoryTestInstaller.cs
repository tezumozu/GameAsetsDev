using UnityEngine;
using Zenject;

using My1WeekGameSystems_ver3;

public class CoroutineMemoryTestInstaller : MonoInstaller{
    public override void InstallBindings(){
        var gameManager = new TestPausableObject();

        //GameManager
        Container
            .Bind<I_Pausable>()
            .To<TestPausableObject>()
            .FromInstance(gameManager);

        Container
            .Bind<I_SceneLoadNoticable>()
            .To<TestPausableObject>()
            .FromInstance(gameManager);

        Container
            .Bind<TestPausableObject>()
            .To<TestPausableObject>()
            .FromInstance(gameManager);

        
        
    }
}