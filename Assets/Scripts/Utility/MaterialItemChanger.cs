using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialItemChanger : MonoBehaviour
{
    [SerializeField] private Image m_TargetImage;
    [SerializeField] private Image m_ImageUsed;

    public void ChangeMaterialSprite()
    {
        this.m_TargetImage.sprite = this.m_ImageUsed.sprite; 
    }
}
