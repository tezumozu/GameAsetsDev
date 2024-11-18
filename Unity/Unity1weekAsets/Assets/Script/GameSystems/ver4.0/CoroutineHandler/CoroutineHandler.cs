using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using Zenject;

namespace Unity1Week_Main_GameSystem_v4{

    /// <summary>
    /// 非MonoBehaviourでも非同期処理（コルーチン）を実現するためのクラス
    /// </summary>
    public class CoroutineHandler : MonoSingleton<CoroutineHandler>{

        //登録されたコルーチンの稼働状況を記録するリスト
        static Dictionary<IEnumerator,bool> activeCoroutineDic;
        //監視用コルーチンのリスト
        static Dictionary<IEnumerator,Coroutine> checkerCoroutineDic;


        [Inject]
        /// <summary>
        /// シーン更新時に呼び出される初期化関数、別の場所で呼び出さない
        /// </summary>
        /// <param name="sceneloadAsync">シーンの読み込み開始を通知するオブジェクト（更新後のゲームマネージャをZenjectで注入）</param>
        private void UpdateSceneInjection(I_SceneLoadNoticable sceneloadAsync){

            //シーンの終了を監視
            sceneloadAsync.SceneLoadAsync.Subscribe((_)=>{
                
                var keys = checkerCoroutineDic.Keys;

                //登録された全てのコルーチンに対して
                foreach ( var coroutine in keys ) {

                    //登録されている全てのコルーチンを停止
                    instance.StopCoroutine(coroutine);

                    //監視用のコルーチンを停止
                    instance.StopCoroutine(checkerCoroutineDic[coroutine]);

                }
                
                //辞書を空にする（これで管理していたコルーチンは破棄される？要検証）
                activeCoroutineDic.Clear();
                checkerCoroutineDic.Clear();

            }).AddTo(instance);

        }



        /// <summary>
        /// オブジェクトが生成されたときに呼び出される初期化処理
        /// </summary>
        protected override void OnInitialize() {

            //各種リストのインスタンスを生成
            activeCoroutineDic = new Dictionary<IEnumerator,bool>();
            checkerCoroutineDic = new Dictionary<IEnumerator,Coroutine>();

        }



        /// <summary>
        /// コルーチンを受け取って開始する、
        /// すでに開始したコルーチンを受け取った場合、何もせずリターン、
        /// 停止しているコルーチンを受け取った場合再開する
        /// </summary>
        /// <param name="coroutine">開始するコルーチン</param>
        public static void OrderStartCoroutine(IEnumerator coroutine){

            //コルーチンが登録されているか？
            if(activeCoroutineDic.ContainsKey(coroutine)) {

                //コルーチンがアクティブか？
                if(activeCoroutineDic[coroutine]){
                    Debug.Log( " CoroutineHandler : This Coroutine is Already Active! " );
                    return ;
                }

                //アクティブでないなら再開する
                instance.StartCoroutine(coroutine);
                activeCoroutineDic[coroutine] = true;
                return ;

            }

            //コルーチンの終了を監視するためのコルーチンを生成する
            Coroutine checkerCoroutine = instance.StartCoroutine ( CheckFinishCoroutine( coroutine ) );
            
            //コルーチンをリストに登録する
            checkerCoroutineDic.Add( coroutine , checkerCoroutine );
            activeCoroutineDic.Add( coroutine , true );
        }



        /// <summary>
        /// コルーチンを停止する
        /// すでにコルーチンが停止していたら何もしない
        /// コルーチンが登録されていなければ何もしない
        /// </summary>
        /// <param name="coroutine">停止するコルーチン</param>
        public static void OrderStopCoroutine(IEnumerator coroutine){
            
            //コルーチンが登録されていない場合リターン
            if(!activeCoroutineDic.ContainsKey(coroutine)){
                Debug.Log( " CoroutineHandler : Not Registration Coroutine ! " );
                return;
            }
            
            //コルーチンが停止していたらリターン
            if(!activeCoroutineDic[coroutine]){
                Debug.Log( " CoroutineHandler : This Coroutine is Already Active! " );
                return;
            }
            
            instance.StopCoroutine(coroutine);
            activeCoroutineDic[coroutine] = false;

        }



        /// <summary>
        /// 受け取ったコルーチンを完全に停止する
        /// </summary>
        /// <param name="coroutine">停止するコルーチン</param>
        public static void OrderKillCoroutine(IEnumerator coroutine){

            //コルーチンが登録されていない場合リターン
            if(!activeCoroutineDic.ContainsKey(coroutine)){
                Debug.Log( " CoroutineHandler : Not Registration Coroutine ! " );
                return;
            }
            
            //コルーチンがアクティブなら停止
            if(activeCoroutineDic[coroutine]){
                instance.StopCoroutine(coroutine);
                instance.StopCoroutine(checkerCoroutineDic[coroutine]);
                return;
            }

            //終了したので辞書から削除
            activeCoroutineDic.Remove(coroutine);
            checkerCoroutineDic.Remove(coroutine);

        }



        /// <summary>
        /// 開始したコルーチンの終了を監視するコルーチン
        /// </summary>
        /// <param name="coroutine">呼びだすコルーチン</param>
        /// <returns>コルーチン、null</returns>
        private static IEnumerator CheckFinishCoroutine(IEnumerator coroutine){

            //呼び出し元から受け取ったコルーチンを開始する
            var activeCoroutine = instance.StartCoroutine(coroutine);
            
            //同じフレーム内で辞書の追加と削除を同時に行わないようにする
            yield return null ;

            //呼び出し元のコルーチンの終了を待つ
            yield return activeCoroutine ;

            //終了したので辞書から削除
            activeCoroutineDic.Remove(coroutine);
            checkerCoroutineDic.Remove(coroutine);
        }



        /// <summary>
        /// 受け取ったコルーチンが登録されているか
        /// ＝コルーチンが終了しているか成否を返す。
        /// </summary>
        /// <param name="coroutine">対象のコルーチン</param>
        /// <returns> 
        /// ture：登録されている＝終了していない
        /// false：登録されていない＝コルーチンの終了
        /// </returns>
        public static bool isFinishCoroutine(IEnumerator coroutine){
            return !activeCoroutineDic.ContainsKey(coroutine);
        }

    }
}


