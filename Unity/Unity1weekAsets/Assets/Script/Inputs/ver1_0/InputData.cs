using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My1WeekGameSystems_ver1;

namespace MyInputManager_ver1{
    public class InputData {
        public E_InputType type;
        public float frameCount;

        public InputData (E_InputType type){
            this.type = type;
            this.frameCount = 0.0f;
        }
    }
}
