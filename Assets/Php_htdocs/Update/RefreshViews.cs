using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RefreshViews : MonoBehaviour
{
    private void Awake()
    {
        this.Refresh();   
    }


    public void Refresh()
    {
        this.StartCoroutine(DoViewsRefresh());
    }

    private IEnumerator DoViewsRefresh()
    {
        using (UnityWebRequest handler = UnityWebRequest.Get("http://localhost/Update/RefreshViews.php"))
        {
            yield return handler.SendWebRequest();

            if(handler.error == null)
            {
                Debug.Log("Refreshed views");
            }
            else
            {
                Debug.LogError(handler.error);
                Debug.LogError(handler.downloadHandler.text);
            }
        }
    }
}
