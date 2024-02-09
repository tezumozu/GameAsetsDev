using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using MyInputManager_ver1;

namespace My1WeekGameSystems_ver1{
    public abstract class SceneObjectUpdateManager{
        //DI
        [Inject] protected GameManager gameManager;
        [Inject] protected I_InputUpdatable inputManager;
        

        public abstract void InitObject();

        public abstract void UpdateObject();

        public abstract void ReleaseObject();
    }
}
