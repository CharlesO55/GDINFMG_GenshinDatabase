using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class DistinctValues : MonoBehaviour
{
    public static DistinctValues Instance;
    
    public static Dictionary<EColNames, List<string>> DistinctColValues {  get; private set; }
    public static Dictionary<string, int> DICT_COUNT_CHAR_NAMES { get; private set; }


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DistinctColValues = new() 
        {
            { EColNames.CHAR_NAMES, new()}, 
            { EColNames.REGIONS, new()},
            { EColNames.VISIONS, new()},
            { EColNames.SPECIAL_STATS, new()},
            { EColNames.GATHER_MATS, new()}, 
            { EColNames.GEM_MATS, new()}, 
            { EColNames.MOB_MATS, new()}, 
            { EColNames.BOSS_MATS, new()} 
        };
        this.StartCoroutine(LoadDistinctColValues("character_name", EColNames.CHAR_NAMES));
        this.StartCoroutine(LoadDistinctColValues("region", EColNames.REGIONS));
        this.StartCoroutine(LoadDistinctColValues("vision", EColNames.VISIONS));
        this.StartCoroutine(LoadDistinctColValues("ascension", EColNames.SPECIAL_STATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_specialty", EColNames.GATHER_MATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_gem", EColNames.GEM_MATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_material", EColNames.MOB_MATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_boss", EColNames.BOSS_MATS));


        SceneManager.sceneLoaded += UpdateCountValue;
        DontDestroyOnLoad(this);
    }

    void UpdateCountValue(Scene scene, LoadSceneMode _)
    {
        if(scene.name == "MainScreen")
        {
            this.StartCoroutine(CountValues("character_name", EColNames.CHAR_NAMES));
        }

    }
    
    IEnumerator CountValues(string colName, EColNames eColNames)
    {
        WWWForm form = new();
        form.AddField("FIELD_ColName", colName);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/CountValues.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error == null)
            {
                switch (eColNames)
                {
                    case EColNames.CHAR_NAMES:
                        DICT_COUNT_CHAR_NAMES = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(handler.downloadHandler.text);
                        break;
                    default:
                        Debug.LogError($"Count values doesn't handle {eColNames}");
                        break;
                }
            }
            else
                Debug.LogError(handler.error);
        }
    }


    IEnumerator LoadDistinctColValues(string colName, EColNames EColName)
    {
        WWWForm form = new();
        form.AddField("FIELD_ColName", colName);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/DistinctValues.php", form))
        {
            yield return handler.SendWebRequest();

            if(handler.error == null)
            {
                StoreResults(handler.downloadHandler.text, EColName);
            }
            else
                Debug.LogError(handler.error);
        }
    }

    void StoreResults(string result, EColNames EColName) 
    {
        string[] results = result.Split('|');
        foreach (string s in results)
        {
            if(s != "")
            {
                DistinctColValues[EColName].Add(s);
            }
        }
    }
}
