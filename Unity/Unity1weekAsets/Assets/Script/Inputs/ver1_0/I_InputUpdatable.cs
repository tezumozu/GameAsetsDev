using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyInputManager_ver1{
    public interface I_InputUpdatable {
        public abstract void UpdateInput();
        public abstract InputData[] GetInputList();
        public abstract InputData[] GetInputBuffer();
    }
}
