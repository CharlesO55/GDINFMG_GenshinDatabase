using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class FilterCharacters : MonoBehaviour
{
    [SerializeField] string _visionField = "'Geo','Dendro','Cryo','Pyro','Hydro','Electro', 'Anemo'";
    [SerializeField] string _regionField = "'Mondstadt', 'Sumeru', 'N/A', 'Inazuma', 'Liyue', 'Fontaine', 'Snezhnaya'";
    [SerializeField] string _weaponField = "'Sword','Bow','Claymore','Catalyst','Polearm'";
    [SerializeField] string _modelField = "'Medium Male','Tall Male','Medium Female','Tall Female','Short Female'";

    void Start()
    {
        this.StartCoroutine(DoFilterCharacters());
    }

    private IEnumerator DoFilterCharacters()
    {
        WWWForm form = new();
        form.AddField("FIELD_Visions", this._visionField);
        form.AddField("FIELD_Regions", this._regionField);
        form.AddField("FIELD_Weapons", this._weaponField);
        form.AddField("FIELD_Models", this._modelField);


        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/filterCharacters.php", form))
        {
            yield return handler.SendWebRequest();

            string result = handler.downloadHandler.text;
            if (result.Contains("SUCCESS"))
            {
                Debug.Log("Character filter passed");
                this.ExtractQueriedCharacters(result.Split('~')[1]);
            }
            else
                Debug.LogError("Filter failed [ERROR]: " + result);
        }
    }

    void ExtractQueriedCharacters(string results)
    {
        foreach(string res in results.Split('@'))
        {
            if(res != "")
                Debug.Log(res);
        }
    }
}