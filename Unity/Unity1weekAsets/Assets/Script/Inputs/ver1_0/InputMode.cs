using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Zenject;
using My1WeekGameSystems_ver1;

namespace MyInputManager_ver1{

    public abstract class InputMode : MonoBehaviour{
        protected bool isActive;
        protected Dictionary < E_InputType , bool> isHoldDic;

        //シリアライズ
        [SerializeField]
        protected readonly E_GameMode ownMode;

        //DI
        [Inject]
        protected I_InputDataAddable inputManager;
        
        [Inject]
        protected virtual void InitInput (I_GameModeChangeAlertable gameManager){
            isActive = false;
            isHoldDic = new Dictionary<E_InputType, bool>();

            //ゲームモードの監視
            gameManager.ObserveGameModeChange(changeGameMopde);
        }
        
        protected virtual void changeGameMopde(E_GameMode nextMode){
            if(ownMode == nextMode){
                isActive = true;
            }else{
                isActive = false;
            }
        }
    }

}
