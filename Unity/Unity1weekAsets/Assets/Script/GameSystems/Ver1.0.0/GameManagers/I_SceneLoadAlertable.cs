using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My1WeekGameSystems_ver1{
   public interface I_SceneLoadAlertable {
      public abstract void ObserveSceneLoadAlert(Action<E_SceneName> action);
   }
}
