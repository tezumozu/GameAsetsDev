using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

namespace Unity1Week_Main_GameSystem_v4{
    public interface I_SceneLoadNoticable{
        public IObservable<E_SceneName> SceneLoadAsync { get; }
    }
}

