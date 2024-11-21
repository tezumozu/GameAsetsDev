using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading;
using Cysharp.Threading.Tasks;

public class UniTaskTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cts = new CancellationTokenSource();
        var token = cts.Token;
        UniTask.Create(TestTask);
        Debug.Log("UniTaskTest : 1");
    }

    async UniTask TestTask(){
        
        Debug.Log("UniTaskTest : 2");
        await UniTask.Delay(10);
        Debug.Log("UniTaskTest : 3");
    }
}
