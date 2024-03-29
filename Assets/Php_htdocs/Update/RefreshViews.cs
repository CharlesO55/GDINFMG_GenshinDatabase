using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RefreshViews : MonoBehaviour
{
    public static RefreshViews Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.Refresh();
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
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

            if(handler.error != null)
                Debug.LogError(handler.error);

            Debug.Log(handler.downloadHandler.text);
        }
    }
}
