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

    enum EStoreType
    {
        GENERAL,
        STATS
    };

    [Header("General Info")]
    public Dictionary<string, string> DictGeneralStats;
    public string CharName;
    public string CharRarity;
    public string CharVision;
    public string CharWeapon;
    public string CharRegion;
    public string CharConstellation;
    public string CharAffiliation;
    public string CharDescription;

    [Header("Character Stats")]
    public int CharHP;
    public int CharATK;
    public int CharDEF;
    public string CharAscValue;
    public string CharAscStat;

    private int _nCoroutinesRunning = 0;

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
    }
    IEnumerator LoadData(WWWForm form, string url, EStoreType EStore)
    {
        _nCoroutinesRunning++;

        using (UnityWebRequest handler = UnityWebRequest.Post(url, form))
        {
            yield return handler.SendWebRequest();

            if (handler.error == null)
            {
                this.StoreResults(handler.downloadHandler.text.Split('|'), EStore);
            }
            else
                Debug.LogError(handler.error);

            Debug.Log(handler.downloadHandler.text);
        }

        UpdateCoroutinesCount();
    }

    private void StoreResults(string[] results, EStoreType EStore)
    {
        switch (EStore)
        {
            case EStoreType.GENERAL:
                this.CharName = results[0];
                this.CharRarity = results[1];
                this.CharVision = results[2];
                this.CharWeapon = results[3];
                this.CharRegion = results[4];
                this.CharConstellation = results[5];
                this.CharAffiliation = results[6];
                this.CharDescription = results[7];
                break;
            case EStoreType.STATS:
                this.CharAscStat = results[1];
                this.CharAscValue = results[2];
                break;
        }
    }

    private void UpdateCoroutinesCount()
    {
        this._nCoroutinesRunning--;
        if(this._nCoroutinesRunning == 0)
        {
            this.OnLoadingFinished?.Invoke();
        }
    }
}
