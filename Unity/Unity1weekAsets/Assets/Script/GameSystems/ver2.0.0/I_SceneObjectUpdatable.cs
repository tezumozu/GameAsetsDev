using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My1WeekGameSystems_ver2{
    interface I_SceneObjectUpdatable{
        public abstract I_SceneLoadAlertable InitObject();
        public abstract void StartGame();
        public abstract void UpdateObject();
        public abstract void ReleaseObject();
    }
}

