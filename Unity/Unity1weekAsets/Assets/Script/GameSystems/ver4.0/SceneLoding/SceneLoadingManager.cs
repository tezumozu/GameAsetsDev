using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Unity1Week_MainGameSystem_v4{

    /// <summary>
    /// シーンのロードに関する処理を扱うクラス
    /// </summary>
    public class SceneLoadingManager {

        private AsyncOperation asyncLoad;
        private I_LoadingUIUpdatable loadingUI;
        private float currentTime;
        private readonly float loadingDilay = 1.0f;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="loadingUI">ロード時に表示するUIを管理するクラス</param>
        public SceneLoadingManager(I_LoadingUIUpdatable loadingUI){
            this.loadingUI = loadingUI;

            this.loadingUI.UpdateProgress(0.0f);
            currentTime = 0;
        }


        /// <summary>
        /// シーンを読み込むコルーチン
        /// </summary>
        /// <param name="sceneName">読み込むシーンを示したEnum</param>
        /// <returns>コルーチン</returns>
        public IEnumerator LoadScene(E_SceneName sceneName){

            //ローディングアニメーションの開始
            loadingUI.StartLoadingAnim();

            //シーン名を文字列へ変換
            string str = Enum.GetName(typeof(E_SceneName),sceneName);

            //シーン読み込み開始
            asyncLoad = SceneManager.LoadSceneAsync(str);

            asyncLoad.allowSceneActivation = false;

            //演出としてローディングの時間を一定時間確保する
            while( asyncLoad.progress < 0.9f || currentTime < loadingDilay ){

                currentTime += Time.deltaTime;
                loadingUI.UpdateProgress(asyncLoad.progress * (currentTime / loadingDilay) + 0.1f);
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
        }
    }

}
