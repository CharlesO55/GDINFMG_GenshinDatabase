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
    public UnityEvent OnDatabaseUpdateFinished;

    private int _nCoroutinesRunning = 0;

    enum EStoreType
    {
        GENERAL,
        STATS,
        LEVELING_REQS
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
                /*this.StartCoroutine(GetItemID(LevelingData.Name_Boss, LevelingData.ID_Boss, EStoreType.BOSS_MATS));
                this.StartCoroutine(GetItemID(LevelingData.Name_Mob, LevelingData.ID_Mob, EStoreType.MOB_MATS));
                this.StartCoroutine(GetItemID(LevelingData.Name_Gather, LevelingData.ID_Gather, EStoreType.GATHER_MATS));
                this.StartCoroutine(GetItemID(LevelingData.Name_Gem, LevelingData.ID_Gem, EStoreType.GEM_MATS));*/

                this.UpdateItemIDs(EColNames.BOSS_MATS);
                this.UpdateItemIDs(EColNames.MOB_MATS);
                this.UpdateItemIDs(EColNames.GATHER_MATS);
                this.UpdateItemIDs(EColNames.GEM_MATS);
                break;
        }
    }

    public void UpdateItemIDs(EColNames E_ITEM_COL_NAME)
    {
        switch (E_ITEM_COL_NAME)
        {
            case EColNames.BOSS_MATS:
                this.StartCoroutine(GetItemID(LevelingData.Name_Boss, LevelingData.ID_Boss, E_ITEM_COL_NAME));
                break;
            case EColNames.MOB_MATS:
                this.StartCoroutine(GetItemID(LevelingData.Name_Mob, LevelingData.ID_Mob, E_ITEM_COL_NAME));
                break;
            case EColNames.GATHER_MATS:
                this.StartCoroutine(GetItemID(LevelingData.Name_Gather, LevelingData.ID_Gather, E_ITEM_COL_NAME));
                break;
            case EColNames.GEM_MATS:
                this.StartCoroutine(GetItemID(LevelingData.Name_Gem, LevelingData.ID_Gem, E_ITEM_COL_NAME));
                break;
        }
    }
    private IEnumerator GetItemID(string inName, string inID, EColNames E_ITEM_COL_NAME)//EStoreType EStore)
    { 
        this._nCoroutinesRunning++;


        WWWForm form = new();
        form.AddField("FIELD_Name", Regex.Replace(inName, "[^\\w\\._]", ""));
        //form.AddField("FIELD_Name", TextCleaner.ParseAlphanumeric(inName, true, "None"));
        form.AddField("FIELD_Id", inID);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/GetItemID.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error == null)
            {
                //Debug.LogWarning($"{inName}|{inID}=-----={handler.downloadHandler.text}");
                string[] result = handler.downloadHandler.text.Split('|');

                switch (E_ITEM_COL_NAME)
                {
                    case EColNames.BOSS_MATS:
                        LevelingData.Name_Boss = result[0];
                        LevelingData.ID_Boss = result[1];
                        break;
                    case EColNames.MOB_MATS:
                        LevelingData.Name_Mob= result[0];
                        LevelingData.ID_Mob = result[1];
                        break;
                    case EColNames.GATHER_MATS:
                        LevelingData.Name_Gather= result[0];
                        LevelingData.ID_Gather = result[1];
                        break;
                    case EColNames.GEM_MATS:
                        LevelingData.Name_Gem = result[0];
                        LevelingData.ID_Gem = result[1];
                        break;
                }
            }
            else
                Debug.LogError($"Failed to get item id for {inName}");

            UpdateCoroutinesCount();
        }
    }
    private void UpdateCoroutinesCount()
    {
        this._nCoroutinesRunning--;
        if(this._nCoroutinesRunning <= 0)
        {
            _nCoroutinesRunning = 0;
            this.OnLoadingFinished?.Invoke();
            this.OnLoadingFinished.RemoveAllListeners();
        }
    }



    public void CallUpdateCharacterDatabase()
    {
        this.StartCoroutine(UploadSelectedCharacterInfo());
    }
    private IEnumerator UploadSelectedCharacterInfo()
    {
        WWWForm form = new();

        //Debug.LogWarning(JsonUtility.ToJson(this.GeneralData));
        form.AddField("JSON_GenData", JsonUtility.ToJson(this.GeneralData));
        form.AddField("JSON_StatsData", JsonUtility.ToJson(this.StatsData));
        form.AddField("JSON_LvlData", JsonUtility.ToJson(this.LevelingData));


        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/UpdateCharacter.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error != null)
                Debug.LogError($"Character update failed {handler.error}");
            else
                Debug.LogWarning($"RESULT: {handler.downloadHandler.text}");
        }

        this.OnDatabaseUpdateFinished?.Invoke();
    } 
}
