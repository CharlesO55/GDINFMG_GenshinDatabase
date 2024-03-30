using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using static UnityEditor.Progress;

public class SelectCharacter : MonoBehaviour
{
    public static SelectCharacter Instance;
    
    public UnityEvent OnLoadingFinished;

    private int _nCoroutinesRunning = 0;

    bool _isGeneralDataUpdating = false;
    enum EStoreType
    {
        GENERAL,
        STATS,
        LEVELING_REQS,
        BOSS_MATS,
        MOB_MATS,
        GATHER_MATS,
        GEM_MATS
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

        _isGeneralDataUpdating = true;
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
                _isGeneralDataUpdating = false;
                break;
            case EStoreType.STATS:
                this.StatsData = JsonUtility.FromJson<CharacterStatsData>(result);
                break;
            case EStoreType.LEVELING_REQS:
                this.LevelingData = JsonUtility.FromJson<CharacterLevelingData>(result);
                this.StartCoroutine(GetItemID(LevelingData.Req_Boss, LevelingData.ID_Boss, EStoreType.BOSS_MATS));
                //this.StartCoroutine(GetItemID(LevelingData.Req_Mob, LevelingData.ID_Mob, EStoreType.MOB_MATS));
                //this.StartCoroutine(GetItemID(LevelingData.Req_Gather, LevelingData.ID_Gather, EStoreType.GATHER_MATS));
                this.StartCoroutine(GetItemID(LevelingData.Req_Gem, LevelingData.ID_Gem, EStoreType.GEM_MATS));
                break;
        }
    }

    private IEnumerator GetItemID(string inName, string inID, EStoreType EStore)
    { 
        this._nCoroutinesRunning++;

        if(EStore == EStoreType.GEM_MATS)
        {
            while (_isGeneralDataUpdating)
            {
                yield return null;
            }
            inName = LevelingData.SetGem(GeneralData.Vision);
        }

        WWWForm form = new();
        form.AddField("FIELD_Name", Regex.Replace(inName, "[^\\w\\._]", ""));
        form.AddField("FIELD_Id", inID);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/GetItemID.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error == null)
            {
                string[] result = handler.downloadHandler.text.Split('|');

                switch (EStore)
                {
                    case EStoreType.BOSS_MATS:
                        LevelingData.Req_Boss = result[0];
                        LevelingData.ID_Boss = result[1];
                        break;
                    case EStoreType.MOB_MATS:
                        LevelingData.Req_Mob= result[0];
                        LevelingData.ID_Mob = result[1];
                        break;
                    case EStoreType.GATHER_MATS:
                        LevelingData.Req_Gather= result[0];
                        LevelingData.ID_Gather = result[1];
                        break;
                    case EStoreType.GEM_MATS:
                        //LevelingData.= result[0];
                        LevelingData.ID_Gem = result[1];
                        break;
                }
                
                //Debug.LogWarning(result[0] + result[1]);
            }
            else
                Debug.LogError($"Failed to get item id for {inName}");

            UpdateCoroutinesCount();
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
