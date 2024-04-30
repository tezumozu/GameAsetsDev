using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PlayFab;
using PlayFab.ClientModels;

namespace TestPlayFab{

    public class TestPlayFab_LoginSystem : MonoBehaviour
    {
        void Start(){
            PlayFabClientAPI.LoginWithCustomID(
                new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true }
            , result => Debug.Log("おめでとうございます！ログイン成功です！")
            , error => Debug.Log("ログイン失敗...(´；ω；｀)"));
        }
    }

}
