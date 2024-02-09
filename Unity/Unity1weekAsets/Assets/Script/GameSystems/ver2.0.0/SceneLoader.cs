using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

namespace My1WeekGameSystems_ver2{
    public class SceneLoader {
        private AsyncOperation asyncLoad;
        private Slider loadingSlider;
        private float currentTime;
        private const float loadingDilay = 2.0f;

        public SceneLoader(GameObject loadingSliderObject){
            loadingSlider = loadingSliderObject.GetComponent<Slider>();
            loadingSlider.value = 0;
            currentTime = 0;
        }
        
        public IEnumerator LoadScene(E_SceneName sceneName){
            //シーン読み込み開始
            asyncLoad = SceneManager.LoadSceneAsync(Enum.GetName(typeof(E_SceneName),sceneName));

            asyncLoad.allowSceneActivation = false;

            //演出としてローディングの時間を一定時間確保する
            while( asyncLoad.progress < 0.9f || currentTime < loadingDilay ){
                currentTime += Time.deltaTime;
                loadingSlider.value = asyncLoad.progress * (currentTime / loadingDilay) + 0.1f;
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
        }
    }
}
