using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My1WeekGameSystems_ver3{
    [Serializable]
    public struct S_SoundOptionData{
        public readonly float Sound;
        public readonly float SE;
        public readonly float BGM;

        public S_SoundOptionData(float sound, float se, float bgm){
            Sound = sound;
            SE = se;
            BGM = bgm;
        }
    }
}
