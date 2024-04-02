using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class CreateItemsTable : MonoBehaviour
{
    [Serializable]
    public class ItemListJson
    {
        public List<ItemData> Items;
    }

    public static CreateItemsTable Instance;
    public UnityEvent<string> OnDatabaseActionCompleted;

    private int _nTaskCompleted;
    [SerializeField] private ItemListJson _bossMaterials = new();
    [SerializeField] private ItemListJson _mobMaterials = new();
    [SerializeField] private ItemListJson _gatherMaterials = new();
    [SerializeField] private ItemListJson _gemMaterials = new();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void CallUpdateItemsTable()
    {
        Debug.LogWarning("Creating items library database");
        
        this._nTaskCompleted = 0;
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

        UpdateTaskCounter();
    }

    void UpdateTaskCounter()
    {
        this._nTaskCompleted++;

        if(_nTaskCompleted >= 4)
        {
            this.OnDatabaseActionCompleted?.Invoke("[SUCCESS] All 4 Item lists have been uploaded");
        }
    }
}
