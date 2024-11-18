using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using My1WeekGameSystems_ver3;

public class TestPausableObject : I_Pausable , I_SceneLoadNoticable {
    private Subject<bool> testPause  = new Subject<bool>();
    public IObservable<bool> PauseAsync { get{ return testPause ;} }


    private Subject<E_SceneName> testLoad  = new Subject<E_SceneName>();
    public IObservable<E_SceneName> SceneLoadAsync { get{ return testLoad ;} }

    public void ChangeScene(){
        Debug.Log("test");
        testLoad.OnNext(E_SceneName.TitleScene);
    }
}
