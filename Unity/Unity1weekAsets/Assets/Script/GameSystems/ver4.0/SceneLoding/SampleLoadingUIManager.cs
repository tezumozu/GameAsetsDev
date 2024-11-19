using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Unity1Week_MainGameSystem_v4;

public class SampleLoadingUIManager : MonoBehaviour , I_LoadingUIUpdatable{
    [SerializeField]
    Slider loadingSlider;

    private void Start(){
        gameObject.SetActive(false);
    }

    public void UpdateProgress(float value){
        loadingSlider.value = value;
    }

    public void StartLoadingAnim(){
        gameObject.SetActive(true);
        Debug.Log("StartLoadingAnim");
        return;
    }
}
