using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using UniRx;

namespace Unity1Week_MainGameSystem_v4{
    public class GameLoopManager : MonoBehaviour{

        [SerializeField]
        public int GameFrameLate = 60;

        private E_SceneName nextScene;

        private bool isHaveToLoading;
        
        private SceneLoadingManager loadingManager;
        
        private List<IDisposable> disposeList = new List<IDisposable>();

        [Inject]
        private void InjectInit(I_SceneLoadNoticable gameManager , SceneLoadingManager loadingManager){

            var sceneLoadDispose = gameManager.SceneLoadAsync
            .Subscribe( (sceneName) => {
                activeIsHaveToLoading(sceneName);
            });

            disposeList.Add(sceneLoadDispose);
            
            this.loadingManager = loadingManager;
        }


        private void Start(){
            //フレームレートを設定
            Application.targetFrameRate = GameFrameLate;

        }



        //シーン切り替え時の処理
        private void activeIsHaveToLoading(E_SceneName sceneName){
            isHaveToLoading = true;
            nextScene = sceneName;
        }
    }


    
}


