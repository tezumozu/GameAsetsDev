using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using My1WeekGameSystems_ver3;

public class SampleGameManager : GameManager<E_SampleSceneState> , I_Pausable{

    private Subject<bool> pauseSubject = new Subject<bool>();
    public IObservable<bool> PauseAsync{ get{ return pauseSubject;} }

    public SampleGameManager():base(E_SampleSceneState.Sample){
        
    }


    public override void InitObject(){
        Debug.Log("GameManager : Init");
    }


    public override void StartGame(){
        Debug.Log("GameManager : StartGame");
    }

    public override void ReleaseObject(){
        Debug.Log("GameManager : ReleaseObject");
    }
}
