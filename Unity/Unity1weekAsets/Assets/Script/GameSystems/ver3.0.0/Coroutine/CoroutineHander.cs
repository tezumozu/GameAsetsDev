using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;

using Zenject;

namespace My1WeekGameSystems_ver3{

    public class CoroutineHander : MonoSingleton<CoroutineHander>{
        
        static Dictionary<IEnumerator,Coroutine> ActiveCoroutinDic;
        static Subject<IEnumerator> FinishCoroutinSubject;

        //シーンが更新された際の処理
        [Inject]
        private void UpdateSceneInjection(I_Pausable pauseAsync , I_SceneLoadNoticable sceneloadAsync){
            
            //ポーズになったらコルーチンを止める
            pauseAsync.PauseAsync.Subscribe((flag)=>{
                
                if(flag){
                    //ポーズになったらコルーチンを止める
                    StopAllActiveCoroutine();

                }else{
                    //ポーズが解けたらコルーチン再開
                    ReStartAllActiveCoroutine();

                }

            }).AddTo(instance);


            //シーンの終了を監視
            sceneloadAsync.SceneLoadAsync.Subscribe((_)=>{
                //全てのコルーチンを停止して
                StopAllActiveCoroutine();

                //辞書を空にする
                ActiveCoroutinDic.Clear();

            }).AddTo(instance);

        }



        //初期化処理
        protected override void OnInitialize() {
            ActiveCoroutinDic = new Dictionary<IEnumerator,Coroutine>();
            FinishCoroutinSubject = new Subject<IEnumerator>();

            FinishCoroutinSubject.Subscribe((coroutine)=>{
                //終了したコルーチンをリストから削除する
                ActiveCoroutinDic.Remove(coroutine);
            })
            .AddTo(this);
        }



        //コルーチンを開始する
        public static Coroutine OrderStartCoroutine(IEnumerator coroutine){
            var result = instance.StartCoroutine(CheckFinishCoroutine(coroutine));
            return result;
        }

        //コルーチンを停止する
        public static void OrderStopCoroutine(IEnumerator target){
            //コルーチンが登録されていない場合リターン
            if(!ActiveCoroutinDic.ContainsKey(target)) return;
            instance.StopCoroutine(target);
        }

        //コルーチンを再開する
        public static void ReStartCoroutine(IEnumerator target){
            //コルーチンが登録されていない場合リターン
            if(!ActiveCoroutinDic.ContainsKey(target)) return;
            instance.StartCoroutine(target);
        }



        private static void StopAllActiveCoroutine(){
            foreach(var coroutine in ActiveCoroutinDic.Keys){
                instance.StopCoroutine(coroutine);
            }
        }

        private static void ReStartAllActiveCoroutine(){
            foreach(var coroutine in ActiveCoroutinDic.Keys){
                instance.StartCoroutine(coroutine);
            }
        }

        private static IEnumerator CheckFinishCoroutine(IEnumerator coroutine){
            var activeCoroutine = instance.StartCoroutine(coroutine);

            ActiveCoroutinDic.Add(coroutine,activeCoroutine);

            //コルーチンの終了を待つ
            yield return activeCoroutine;

            //終了したコルーチンを通知する
            FinishCoroutinSubject.OnNext(coroutine);
        }

    }

}
