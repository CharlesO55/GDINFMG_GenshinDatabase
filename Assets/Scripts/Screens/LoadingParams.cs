using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingParams
{
    public EScenes ENextScene { get; private set; }

    public bool IsReloadMasterlistTable { get; private set; }
    public bool IsReloadItemsTable { get; private set; }
    public bool IsReloadViews { get; private set; }
    public bool IsReloadDistinctValues { get; private set; }
    public LoadingParams(EScenes nextScene,  bool isReloadMaster, bool isReloadItems, bool isReloadViews, bool isReloadDistinct)
    {
        ENextScene = nextScene;
        IsReloadMasterlistTable = isReloadMaster;
        IsReloadItemsTable = isReloadItems;
        IsReloadViews = isReloadViews;
        IsReloadDistinctValues = isReloadDistinct;
    }
}