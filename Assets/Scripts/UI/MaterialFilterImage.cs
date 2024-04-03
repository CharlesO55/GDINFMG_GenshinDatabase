using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MaterialFilterImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string itemID;

    private void Start()
    {
        itemID = this.GetComponent<Image>().sprite.name.Replace("UI_ItemIcon_", "");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        FilterCharacters.Instance.CallFilterByMaterial(itemID);
    }
}
