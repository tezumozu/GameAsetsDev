using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My1WeekGameSystems_ver1{
   public interface I_GameModeChangeAlertable {
      public abstract void ObserveGameModeChange(Action<E_GameMode> action);
   }
}
