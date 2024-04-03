using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;


    private Dictionary<EScenes, string> _DictSceneNames = new()
    {
        { EScenes.START_SCREEN, "StartScreen"},
        { EScenes.LOADING_SCREEN, "LoadingScreen"},
        { EScenes.MAIN_SCREEN, "MainScreen"},
        { EScenes.CHAR_VIEW_SCREEN, "CharacterViewScreen"},
        { EScenes.CHAR_EDIT_SCREEN, "CharacterEditScreen"},
        { EScenes.SUMMARY_SCREEN, "SummaryScreen"}
    };

    private LoadingParams _currLoadingParams;
    private TextMeshProUGUI _currLoadingText;
    private GrayscaleProgressBar _currLoadingBar;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += GetNewProgressGUI;
            SceneManager.sceneLoaded += DoDatabaseUpdates;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    void GetNewProgressGUI(Scene scene, LoadSceneMode _)
    {
        if(scene.name == "LoadingScreen")
        {
            this._currLoadingBar = GameObject.FindAnyObjectByType<GrayscaleProgressBar>();
            this._currLoadingBar.Init(4);

            GameObject textObject = GameObject.Find("LoadingProgressText");
            if (textObject != null)
            {
                textObject.TryGetComponent<TextMeshProUGUI>(out this._currLoadingText);
            }
        }
    }
    

    //CALLED ON EVERY SCENE LEVEL LOADED
    void DoDatabaseUpdates(Scene scene, LoadSceneMode _)
    {
        if(_currLoadingParams != null)
        {
            this.CheckReloadMasterlist();
        }
    }
    
    public void LoadSceneWithoutDatabaseAction(string sceneName)
    {
        EScenes sceneKey = _DictSceneNames.FirstOrDefault(x => x.Value == sceneName).Key;
        this.LoadScene(sceneKey, null);
    }
    public void LoadScene(EScenes eScene, LoadingParams loadingParams)
    {
        _currLoadingParams = loadingParams;
        SceneManager.LoadScene(_DictSceneNames[eScene]);
    }



    void CheckReloadMasterlist()
    {
        LogLoadingProgress("Reloading database masterlist...", true);
        //WAIT FOR MASTERLIST TO FINISH RELOADING
        if (_currLoadingParams.IsReloadMasterlistTable)
        {
            ReloadMasterlist.Instance.OnDatabaseActionCompleted.AddListener(result => {
                ReloadMasterlist.Instance.OnDatabaseActionCompleted.RemoveAllListeners();

                LogLoadingProgress(result);
                CheckReloadViews();
            }) ;
            ReloadMasterlist.Instance.CallReloadMasterlist();
        }
        //CONTINUE IF NOT NEEDED TO RELOAD
        else
            this.CheckReloadViews();
    }


    void CheckReloadViews()
    {
        LogLoadingProgress("Refreshing views...", true);

        //WAIT FOR VIEWS TO FINISH RELOADING
        if (_currLoadingParams.IsReloadViews)
        {
            RefreshViews.Instance.OnDatabaseActionCompleted.AddListener(result => {
                RefreshViews.Instance.OnDatabaseActionCompleted.RemoveAllListeners();

                LogLoadingProgress(result);
                CheckReloadItemSpriteTable();
            });
            RefreshViews.Instance.CallRefresh();
        }
        //CONTINUE IF NOT NEEDED TO RELOAD
        else
            this.CheckReloadItemSpriteTable();
    }

    void CheckReloadItemSpriteTable()
    {
        LogLoadingProgress("Refreshing item sprites table...", true);

        //WAIT FOR ITEM SPRITES TO FINISH RELOADING
        if (_currLoadingParams.IsReloadItemsTable)
        {
            CreateItemsTable.Instance.OnDatabaseActionCompleted.AddListener(result =>
            {
                CreateItemsTable.Instance.OnDatabaseActionCompleted.RemoveAllListeners();

                LogLoadingProgress(result);
                CheckReloadDistinctValues();
            });
            CreateItemsTable.Instance.CallUpdateItemsTable();
        }
        //CONTINUE IF NOT NEEDED TO RELOAD
        else 
            CheckReloadDistinctValues();
    }


    void CheckReloadDistinctValues()
    {
        LogLoadingProgress("Refreshing distinct values...", true);

        //WAIT FOR ITEM SPRITES TO FINISH RELOADING
        if (_currLoadingParams.IsReloadDistinctValues)
        {
            DistinctValues.Instance.OnDatabaseActionCompleted.AddListener(() =>
            {
                DistinctValues.Instance.OnDatabaseActionCompleted.RemoveAllListeners();
                LoadNextScene();

                LogLoadingProgress("[SUCCESS] Refreshed distinct values");
            });
            DistinctValues.Instance.CallRefresh();
        }
        //LAST ITEM TO UPDATE
        else 
            LoadNextScene();
    }

    void LoadNextScene()
    {
        this.LoadScene(_currLoadingParams.ENextScene, null);
    }

    void LogLoadingProgress(string resultText, bool isIncrementProgress = false)
    {
        Debug.Log($"Checking: {resultText}");
        this._currLoadingText.text = resultText;
        
        if(isIncrementProgress)
        {
            this._currLoadingBar.IncrementProgress();
        }
    }
}