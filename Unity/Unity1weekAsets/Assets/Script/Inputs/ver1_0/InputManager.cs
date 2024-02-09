using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using My1WeekGameSystems_ver1;

namespace MyInputManager_ver1{
    
    public class InputManager : I_InputDataAddable , I_InputUpdatable {

        public const float maxValidFrameCount = 6.0f * 1.0f / 60.0f;

        private List< InputData > inputList;

        private List< InputData > inputBuffer;

        public InputManager(){
            //インプットデータの初期化
            inputList = new List<InputData> ();
            inputBuffer = new List<InputData> ();
        }


        // 指定フレーム以内に入力された入力をすべて取得
        public InputData[] GetInputList() { 
            //入力が古い順にソート
            inputList.Sort((x,y) => {
                if(y.frameCount > x.frameCount){
                    return 1;
                }
                return -1;
            });

            //配列にコピー
            InputData[] copy = new InputData[inputList.Count];
            inputList.CopyTo(copy);

            //配列内をクリア
            inputList.Clear();
            return copy;

        }


        public InputData[] GetInputBuffer() { 
            //入力順にソート
            inputBuffer.Sort((x,y) => {
                if(y.frameCount > x.frameCount){
                    return 1;
                }
                    return -1;
            });

            //配列にコピー
            InputData[] copy = new InputData[inputBuffer.Count];
            inputBuffer.CopyTo(copy);

            //inputListをクリア
            inputList.Clear();
            return copy;
        }


        public void UpdateInput(){
            
            //インプットデータの更新
            //入力されてから何フレーム経ったかカウント
            for (int i = 0; i < inputList.Count; i++){
                //フレームカウントを加算
                InputData data  = inputList[i]; 
                data.frameCount += Time.deltaTime;
                inputList[i] = data;
            }

            //バッファの更新
            for (int i = 0; i < inputBuffer.Count; i++){
                //フレームカウントを加算
                InputData data  = inputBuffer[i]; 
                data.frameCount += Time.deltaTime;
                inputBuffer[i] = data;
            }

            //有効フレームをすぎた入力を削除
            inputList.RemoveAll( x => {
                return x.frameCount > maxValidFrameCount;
            });

            inputBuffer.RemoveAll( x => {
                return x.frameCount > maxValidFrameCount;
            });
        }


        //入力を登録
        public void SetInputData(E_InputType type){
            InputData data = new InputData(type);
            inputList.Add(data);
            inputBuffer.Add(data);
        }

    }
}
