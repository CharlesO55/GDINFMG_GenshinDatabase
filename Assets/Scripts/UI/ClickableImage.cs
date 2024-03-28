using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickableImage : Image, IPointerClickHandler
{
    protected bool isClicked = true;

    
    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = !isClicked;
        this.OnToggleChange();
    }

    protected virtual void OnToggleChange() { }
}
