using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter Instance;
    
    public UnityEvent OnLoadingFinished;

    private int _nCoroutinesRunning = 0;

    enum EStoreType
    {
        GENERAL,
        STATS,
        LEVELING_REQS,
    };

    [Header("Data Stored")]
    public CharacterGeneralData GeneralData;
    public CharacterStatsData StatsData;
    public CharacterLevelingData LevelingData;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    }


    

    public void LoadSelectedCharacter(string characterName)
    {
        WWWForm form = new WWWForm();
        form.AddField("FIELD_character_name", characterName);

        this.StartCoroutine(LoadData(form, "http://localhost/Retrieve/GetCharacterGeneral.php", EStoreType.GENERAL));
        this.StartCoroutine(LoadData(form, "http://localhost/Retrieve/GetCharacterStats.php", EStoreType.STATS));
        this.StartCoroutine(LoadData(form, "http://localhost/Retrieve/GetCharacterLevelingReqs.php", EStoreType.LEVELING_REQS));
    }
    IEnumerator LoadData(WWWForm form, string url, EStoreType EStore)
    {
        _nCoroutinesRunning++;

        using (UnityWebRequest handler = UnityWebRequest.Post(url, form))
        {
            yield return handler.SendWebRequest();

            if (handler.error == null)
                this.StoreResults(handler.downloadHandler.text, EStore);
            else
                Debug.LogError(handler.error);

            Debug.Log(handler.downloadHandler.text);
        }

        UpdateCoroutinesCount();
    }

    private void StoreResults(string result, EStoreType EStore)
    {
        switch (EStore)
        {
            case EStoreType.GENERAL:
                this.GeneralData = JsonUtility.FromJson<CharacterGeneralData>(result);    
                break;
            case EStoreType.STATS:
                this.StatsData = JsonUtility.FromJson<CharacterStatsData>(result);
                break;
            case EStoreType.LEVELING_REQS:
                this.LevelingData = JsonUtility.FromJson<CharacterLevelingData>(result);
                break;
        }
    }

    private void UpdateCoroutinesCount()
    {
        this._nCoroutinesRunning--;
        if(this._nCoroutinesRunning == 0)
        {
            this.OnLoadingFinished?.Invoke();
            this.OnLoadingFinished.RemoveAllListeners();
        }
    }
}
