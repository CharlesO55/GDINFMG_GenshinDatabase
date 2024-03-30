using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class CreateItemsTable : MonoBehaviour
{
    [Serializable]
    public class ItemListJson
    {
        public List<ItemData> Items;
    }

    [SerializeField] private ItemListJson _bossMaterials = new();
    [SerializeField] private ItemListJson _mobMaterials = new();
    [SerializeField] private ItemListJson _gatherMaterials = new();
    [SerializeField] private ItemListJson _gemMaterials = new();


    private void Start()
    {
        Debug.LogWarning("Creating items library database");
        this.CleanAndUpload(_gemMaterials);
        this.CleanAndUpload(_gatherMaterials);
        this.CleanAndUpload(_mobMaterials);
        this.CleanAndUpload(_bossMaterials);
    }

    void CleanAndUpload(ItemListJson listJson)
    {
        foreach (ItemData item in listJson.Items)
        {
            item.ItemName = Regex.Replace(item.ItemName, "[^\\w\\._]", "");
            item.ItemID = item.Sprite.name.Replace("UI_ItemIcon_", "");
        }
        this.StartCoroutine(this.DoTableUpdate(listJson));
    }

    private IEnumerator DoTableUpdate(ItemListJson itemList)
    {
        WWWForm form = new WWWForm();
        
        form.AddField("FIELD_Items", JsonUtility.ToJson(itemList));

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/UpdateItemTables.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error != null)
                Debug.LogError(handler.error);

            Debug.Log(handler.downloadHandler.text);
        }
    }
}
