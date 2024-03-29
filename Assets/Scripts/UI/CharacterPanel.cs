using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TextMeshProUGUI _charName;
    [SerializeField] Image _charIcon;
    [SerializeField] Image _charRarity;

    public void Initialize(string name, Sprite icon, int rarity = 4)
    {
        //Debug.Log($"{name} {rarity}");
        this._charName.text = name;
        this._charRarity.color = (rarity == 5) ? new Color32(208, 144, 64, 255) : new Color32(150, 65, 210, 255);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectCharacter.Instance.LoadSelectedCharacter(this._charName.text);

        SelectCharacter.Instance.OnLoadingFinished.AddListener(() => {
            SceneLoader.Instance.LoadScene("CharacterViewScreen");
        });
    }
}