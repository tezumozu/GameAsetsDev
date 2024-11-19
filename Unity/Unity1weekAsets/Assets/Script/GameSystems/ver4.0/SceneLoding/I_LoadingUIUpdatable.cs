using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_LoadingUIUpdatable {
   public void UpdateProgress(float value);
   public void StartLoadingAnim();
}
