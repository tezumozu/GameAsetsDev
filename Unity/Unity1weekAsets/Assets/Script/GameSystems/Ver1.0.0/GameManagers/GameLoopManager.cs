using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;

using Zenject;

namespace My1WeekGameSystems_ver1 {
    public class GameLoopManager : MonoBehaviour {

        private E_LoopState currentState;
        private bool isHaveToLoading;
        private UniTask initAsync;
        private SceneLoader sceneLoader;

        //シーン読み込み用
        private E_SceneName nextScene;

        //DI
        [Inject] SceneObjectUpdateManager UpdateManager;

        //DI
        [Inject] 
        void OnInitialize(I_SceneLoadAlertable gameManager){
            //シーン切り替えを監視
            gameManager.ObserveSceneLoadAlert(activeIsHaveToLoading);
        }


        //各シーンごとの初期化
        void Start(){
            sceneLoader = new SceneLoader();
            currentState = E_LoopState.Init;

            //初期化処理を実行
            initAsync = UniTask.RunOnThreadPool(()=>{
                UpdateManager.InitObject();
            });
        }


        //各フレームごとの処理
        void Update(){
            switch(currentState){

                //初期化処理
                case E_LoopState.Init :

                    //初期化の終了を待つ
                    if(initAsync.GetAwaiter().IsCompleted){
                        //初期化が終了したら切り替え
                        currentState = E_LoopState.Update;
                    }

                    break;



                //Update処理
                case E_LoopState.Update :

                    //オブジェクトをUpdate
                    UpdateManager.UpdateObject();

                    //シーンロードが必要ならば
                    if(isHaveToLoading){
                        currentState = E_LoopState.Loading;

                        //不要なオブジェクトを開放する　(C#側)
                        UpdateManager.ReleaseObject();

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
        private void activeIsHaveToLoading(E_SceneName stageName){
            isHaveToLoading = true;
            nextScene = stageName;
        }

        //強制終了時の処理


        private enum E_LoopState {
            Init,
            Update,
            Loading
        }
    }
}

