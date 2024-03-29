using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QueryValues))]
public class QueryImage : ClickableImage
{
    QueryValues _queryToggle;
    protected override void Awake()
    {
        base.Awake();
        this._queryToggle = GetComponent<QueryValues>();
    }

    protected override void OnToggleChange()
    {
        this.color = isClicked ? Color.white : Color.gray;


        if(FilterCharacters.Instance != null)
            FilterCharacters.Instance.ModifyFilter(_queryToggle.key, _queryToggle.value, isClicked);
    }
}
