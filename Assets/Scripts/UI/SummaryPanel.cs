using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SummaryPanel : MonoBehaviour
{
    [SerializeField] Image _imgBorder;
    [SerializeField] Image _imgBody;
    [SerializeField] Image _imgSpriteIcon;

    [SerializeField] TextMeshProUGUI _parameter;
    [SerializeField] TextMeshProUGUI _atkValue;
    [SerializeField] TextMeshProUGUI _defValue;
    [SerializeField] TextMeshProUGUI _hpValue;
    [SerializeField] TextMeshProUGUI _countsValue;

    public void SetValues(string parameter, string atk, string def, string hp, string counts, string imgName)
    {
        _parameter.text = parameter;
        _atkValue.text = atk;
        _defValue.text = def;
        _hpValue.text = hp;
        _countsValue.text = counts;

        _imgBody.enabled = true; 
        _imgBorder.enabled = true;

        ImageLoader.LoadImageInResources(_imgSpriteIcon, imgName);
    }
}
