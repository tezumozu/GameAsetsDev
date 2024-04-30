using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using My1WeekGameSystems_ver3;

public class SampleGameManager : GameManager<E_SampleSceneState>{
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
