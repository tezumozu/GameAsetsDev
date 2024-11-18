using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using Zenject;
using My1WeekGameSystems_ver3;

public class CoroutineMemoryTest2 : MonoBehaviour{

    [Inject]
    TestPausableObject tester;

    void Start(){
        //100個コルーチンを作成
        for(int i  = 0; i < 10000; i++){
            CoroutineHander.OrderStartCoroutine( TestCoroutine() , true );
        }
    }

    void Update(){
        //スペースを押したらシーンを切り替える
        if (Input.GetKeyDown(KeyCode.Space)) {

            tester.ChangeScene();
            SceneManager.LoadScene("CoroutineMemory1");
        }
    }

    private IEnumerator TestCoroutine (){
        yield return new WaitForSeconds(60f);
    }
}
