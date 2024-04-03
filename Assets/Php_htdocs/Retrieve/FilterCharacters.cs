using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class FilterCharacters : MonoBehaviour
{
    //[SerializeField] string _visionField = "'Geo','Dendro','Cryo','Pyro','Hydro','Electro', 'Anemo'";
    //[SerializeField] string _regionField = "'Mondstadt', 'Sumeru', 'N/A', 'Inazuma', 'Liyue', 'Fontaine', 'Snezhnaya'";
    //[SerializeField] string _weaponField = "'Sword','Bow','Claymore','Catalyst','Polearm'";
    //[SerializeField] string _modelField = "'Medium Male','Tall Male','Medium Female','Tall Female','Short Female'";

    [SerializeField] GameObject _characterPanelPrefab;
    [SerializeField] Transform _characterPanelsContainer;

    [SerializeField] string _nameSearchField = "";

    List<int> _rarities;
    Dictionary<string, List<string>> _dictQueries = new();

    public static FilterCharacters Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        this._rarities = new() { 4, 5 };
        this._dictQueries.Add("Vision", new() { "'Geo'", "'Dendro'", "'Cryo'", "'Pyro'", "'Hydro'", "'Electro'", "'Anemo'" });
        this._dictQueries.Add("Region", new() { "'Mondstadt'", "'Sumeru'", "'N/A'", "'Inazuma'", "'Liyue'", "'Fontaine'", "'Snezhnaya'" });
        this._dictQueries.Add("Weapon", new() { "'Sword'", "'Bow'", "'Claymore'", "'Catalyst'", "'Polearm'" });
        this._dictQueries.Add("Model", new() { "'Medium Male'","'Tall Male'","'Medium Female'","'Tall Female'","'Short Female'" });
        this.CallFilterByMultiple();
    }

    public void CallFilterByMultiple()
    {
        this.StartCoroutine(DoFilterCharacters());
    }

    public void CallFilterByMaterial(string itemID)
    {
        this.StartCoroutine(DoFilterByMaterials(itemID));
    }

    private IEnumerator DoFilterByMaterials(string itemID)
    {
        WWWForm form = new();
        form.AddField("FIELD_ItemID", itemID);
        
        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/FilterByMaterials.php", form))
        {
            yield return handler.SendWebRequest();
            DeleteCurrentPanels();


            string[][] results = JsonConvert.DeserializeObject<string[][]>(handler.downloadHandler.text);
            foreach (string[] character in results)
            {
                GameObject newCharPanel = Instantiate(this._characterPanelPrefab, this._characterPanelsContainer);
                if (newCharPanel.TryGetComponent<CharacterPanel>(out CharacterPanel panel))
                {
                    panel.Initialize(character[0], int.Parse(character[1]));
                }
            }
        }
    }

    private IEnumerator DoFilterCharacters()
    {
        WWWForm form = new();
        form.AddField("FIELD_Rarities", this.ExtractFilterString(this._rarities));
        form.AddField("FIELD_Visions", this.ExtractFilterString(this._dictQueries["Vision"]));
        form.AddField("FIELD_Regions", this.ExtractFilterString(this._dictQueries["Region"]));
        form.AddField("FIELD_Weapons", this.ExtractFilterString(this._dictQueries["Weapon"]));
        form.AddField("FIELD_Models", this.ExtractFilterString(this._dictQueries["Model"]));
        form.AddField("FIELD_CharacterName", CleanCharacterSearch(this._nameSearchField));

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/FilterCharacters.php", form))
        {
            yield return handler.SendWebRequest();
            DeleteCurrentPanels();


            string result = handler.downloadHandler.text;
            if (result.Contains("SUCCESS"))
            {
                this.ExtractQueriedCharacters(result.Split('~')[1]);
                Debug.Log($"FILTERED:{result}");
            }
            else
            {
                //Debug.LogWarning("Filter failed [ERROR]: " + handler.downloadHandler.error);
                Debug.LogWarning($"FILTERED Failed:{result}");
            }
        }
    }

    

    void ExtractQueriedCharacters(string results)
    {
        foreach(string res in results.Split('@'))
        {
            if(res != "")
            {
                string[] currChar = res.Split('|');
                GameObject newCharPanel = Instantiate(this._characterPanelPrefab, this._characterPanelsContainer);
                if (newCharPanel.TryGetComponent<CharacterPanel>(out CharacterPanel panel))
                {
                    panel.Initialize(currChar[0], int.Parse(currChar[1]));
                }
            }
        }
    }

    void DeleteCurrentPanels()
    {
        foreach (Transform child in this._characterPanelsContainer.transform)
        {
            if(child.name != "AddCharacterPanel")
                Destroy(child.gameObject);
        }
    }

    string CleanCharacterSearch(string rawCharacterString)
    {
        if (rawCharacterString.Length > 0)
        {
            rawCharacterString = Regex.Replace(rawCharacterString, "[^\\w\\._]", "");
            rawCharacterString = rawCharacterString.Insert(0, "%");
            rawCharacterString = rawCharacterString.Insert(rawCharacterString.Length, "%");
        }
        return rawCharacterString;
    }


    public void ModifyFilter(string key, string value, bool bAdd)
    {
        if(key == "Rarity")
        {
            if (bAdd)
                this._rarities.Add(int.Parse(value));
            else
                this._rarities.Remove(int.Parse(value));
        }
        else 
        {
            if (bAdd)
                this._dictQueries[key].Add(value);
            else
                this._dictQueries[key].Remove(value);
        }

        this.CallFilterByMultiple();
    }

    private string ExtractFilterString<T>(List<T> values)
    {
        string query = "";
        for (int i = 0; i < values.Count; i++)
        {
            query += values[i];
            if (i < values.Count - 1)
            {
                query += ',';
            }
        }

        if (query.Length == 0)
            query = "''";

        //Debug.Log($"Filtering:{query}");
        return query;
    }
}