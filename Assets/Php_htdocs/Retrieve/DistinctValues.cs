using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DistinctValues : MonoBehaviour
{
    public static DistinctValues Instance;
    
    public Dictionary<EColNames, List<string>> DistinctColValues {  get; private set; }

    
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
            { EColNames.VISIONS, new()}, 
            { EColNames.GATHER_MATS, new()}, 
            { EColNames.GEM_MATS, new()}, 
            { EColNames.MOB_MATS, new()}, 
            { EColNames.BOSS_MATS, new()} 
        };
        this.StartCoroutine(LoadDistinctColValues("vision", EColNames.VISIONS));
        this.StartCoroutine(LoadDistinctColValues("ascension_specialty", EColNames.GATHER_MATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_gem", EColNames.GEM_MATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_material", EColNames.MOB_MATS));
        this.StartCoroutine(LoadDistinctColValues("ascension_boss", EColNames.BOSS_MATS));
        
        DontDestroyOnLoad(this);
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
                this.DistinctColValues[EColName].Add(s);
            }
        }
    }
}
