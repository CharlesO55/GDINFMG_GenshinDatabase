using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public void PerformStart(bool isResetMasterlist)
    {
        SceneLoader.Instance.LoadScene(EScenes.LOADING_SCREEN, new LoadingParams(EScenes.MAIN_SCREEN, isResetMasterlist, true, true, true));
    }
}
