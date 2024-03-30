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

    [SerializeField] private bool _isPerformUpdate = false;
    [SerializeField] private ItemListJson _bossMaterials = new();

    private void Start()
    {
        if(!_isPerformUpdate)
        {
            return;
        }

        Debug.LogWarning("Creating items library database");
        foreach (ItemData item in _bossMaterials.Items)
        {
            item.ItemName = Regex.Replace(item.ItemName, "[^\\w\\._]", "");
            item.ItemID = item.Sprite.name.Replace("UI_ItemIcon_", "");
        }

        this.StartCoroutine(this.DoTableUpdate());
    }

    private IEnumerator DoTableUpdate()
    {
        WWWForm form = new WWWForm();
        
        form.AddField("FIELD_BossItems", JsonUtility.ToJson(this._bossMaterials));

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/UpdateItemTables.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error != null)
                Debug.LogError(handler.error);

            Debug.Log(handler.downloadHandler.text);
        }
    }
}
