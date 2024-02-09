using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace My1WeekGameSystems_ver1{
    public abstract class  GameManager : I_SceneLoadAlertable , I_GameModeChangeAlertable{
        protected Subject<E_SceneName> SceneLoadAlertSubject;
        protected Subject<E_GameMode> GameModeChangeAlertSubject;

        public void ObserveSceneLoadAlert(Action<E_SceneName> action){
            SceneLoadAlertSubject.Subscribe( sceneName => {
                action(sceneName);
            });
        }

        public void ObserveGameModeChange(Action<E_GameMode> action){
        GameModeChangeAlertSubject.Subscribe( nextMode => {
                action(nextMode);
            });
        }
        
    }
}
