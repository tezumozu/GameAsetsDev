using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;


namespace Unity1Week_MainGameSystem_v4{
    public abstract class SceneGameManager<T> : I_SceneLoadNoticable 
    where T : Enum
    {
        protected T currentState;
        protected Subject<E_SceneName> SceneLoadSubject;
        public IObservable<E_SceneName> SceneLoadAsync => SceneLoadSubject;

        protected Dictionary< T , GameState<T> > gameStateDic;
        protected List<IDisposable> disposableList;



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneGameManager (){

            SceneLoadSubject = new Subject<E_SceneName>();
            gameStateDic = new Dictionary< T , GameState<T> >();
            disposableList = new List<IDisposable>();

        }


        /// <summary>
        /// ゲームに必要な非MonoBehaviour初期化処理
        /// </summary>
        /// <returns>コルーチン</returns>
        public abstract IEnumerator InitGame();

        /// <summary>
        /// ゲームを開始する
        /// ゲーム開始時の初期化処理
        /// </summary>
        /// <returns>コルーチン</returns>
        public abstract IEnumerator StartGame();

        /// <summary>
        /// シーン遷移時、
        /// 不具合がないよういらないオブジェクトを開放する
        /// </summary>
        public virtual void ReleaseObject(){
            //各状態をDisposeさせる
            foreach(var key in gameStateDic.Keys){
                gameStateDic[key].Dispose();
            }

            //自身の購読を終了する
            foreach(var disposable in disposableList){
                disposable.Dispose();
            }
        }

    }
}


