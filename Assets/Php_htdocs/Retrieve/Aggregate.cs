using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Aggregate : MonoBehaviour
{
    [SerializeField] Transform _summaryPanelContainer;
    [SerializeField] GameObject _summaryPanelPrefab;

    struct WrapperJSONResult
    {
        public string SortingColValue;
        public string CharacterCounts;
        public string MaxAtk;
        public string MaxDef;
        public string MaxHP;
        public string MaxAtkName;
        public string MaxDefName;
        public string MaxHPName;
    }
    private string _columnName;    

    void Start()
    {
        CallAggregation("region");   
    }

    public void CallAggregation(string columnName)
    {
        this._columnName = columnName;
        this.StartCoroutine(DoAggregation());
    }

    IEnumerator DoAggregation()
    {
        WWWForm form = new WWWForm();
        form.AddField("FIELD_SortingColumn", _columnName);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/Aggregate.php", form))
        {
            yield return handler.SendWebRequest();
            EraseCurrSummaryPanels();


            if(handler.error == null)
            {
                Debug.Log(handler.downloadHandler.text);
                UseResults(JsonConvert.DeserializeObject<WrapperJSONResult[]>(handler.downloadHandler.text));
            }
            else
            {
                Debug.LogError($"Aggregation failed {handler.error}");
            }
        }
    }

    void EraseCurrSummaryPanels()
    {
        foreach(Transform child in _summaryPanelContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }


    void UseResults(WrapperJSONResult[] results)
    {
        foreach(WrapperJSONResult result in results)
        {
            string folderPath = this._columnName switch
            {
                "region" => "Regions/" + result.SortingColValue + "_Emblem_Night",
                "weapon_type" => "Weapons/UI_GachaTypeIcon_" + result.SortingColValue,
                "vision" => "Elements/UI_Buff_Element_" + result.SortingColValue,
                "model" => "Models/" + result.SortingColValue,
                "rarity" => "Rarity/Rarity_" + result.SortingColValue,
                _ => "Unknown"
            };

            GameObject newPanel = Instantiate(_summaryPanelPrefab, _summaryPanelContainer);
            newPanel.GetComponent<SummaryPanel>().SetValues(
                result.SortingColValue,
                result.MaxAtk + '\n' + result.MaxAtkName,
                result.MaxDef + '\n' + result.MaxDefName,
                result.MaxHP + '\n' + result.MaxHPName,
                result.CharacterCounts,
                folderPath
                );
        }
    }
}
