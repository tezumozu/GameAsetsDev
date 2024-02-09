using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace My1WeekGameSystems_ver2{
    public class GameLoopManager : MonoBehaviour{
        
        bool isHaveToLoading;

        E_SceneName nextScene;

        E_LoopState currentState;

        private SceneLoader sceneLoader;
        
        [SerializeField] 
        private GameObject loadingUIObject;

        [Inject]
        private I_SceneObjectUpdatable sceneObjectUpdataer;


        //初期化処理
        void Start(){
            //パラメータ初期化
            isHaveToLoading = false;
            currentState = E_LoopState.Init;
            sceneLoader = new SceneLoader(loadingUIObject);
            loadingUIObject.SetActive(false);

            //シーン内のオブジェクト取得・生成・初期化
            I_SceneLoadAlertable gameManager = sceneObjectUpdataer.InitObject();
            
            //シーンの切り替えタイミングを監視
            gameManager.ObserveSceneLoadAlert(activeIsHaveToLoading);

        }


        //各フレームごとの処理
    void Update(){
        switch(currentState){

            //ゲームを開始する
            case E_LoopState.Init :

                currentState = E_LoopState.Update;
                sceneObjectUpdataer.StartGame();

            break;



            //Update処理
            case E_LoopState.Update :
                //オブジェクトをUpdate
                sceneObjectUpdataer.UpdateObject();

                //シーンロードが必要ならば
                if(isHaveToLoading){
                    
                    currentState = E_LoopState.Loading;

                    //不要なオブジェクトを開放する
                    sceneObjectUpdataer.ReleaseObject();

                    //読み込みを開始する
                    StartCoroutine(sceneLoader.LoadScene(nextScene));

                    //読み込みを開始したのでフラグをfalseに
                    isHaveToLoading = false;
                }

                break;



            //Loading時（シーン切り替え時）の処理
            case E_LoopState.Loading :
                //基本待機　何かあれば
                break;
        }
    }


        //シーン切り替え時の処理
        private void activeIsHaveToLoading(E_SceneName sceneName){
            isHaveToLoading = true;
            nextScene = sceneName;
        }


        private enum E_LoopState {
            Init,
            Update,
            Loading
        }
    }
}
