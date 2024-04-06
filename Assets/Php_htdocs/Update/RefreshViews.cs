using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class RefreshViews : MonoBehaviour
{
    public static RefreshViews Instance;
    public UnityEvent<string> OnDatabaseActionCompleted;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void CallRefresh()
    {
        this.StartCoroutine(DoViewsRefresh());
    }

    private IEnumerator DoViewsRefresh()
    {
        //using (UnityWebRequest handler = UnityWebRequest.Get("http://localhost/Update/RefreshViews.php"))
        using (UnityWebRequest handler = UnityWebRequest.Get(ConnectionSettings.SERVER_ADDRESS + "/Update/RefreshViews.php"))
        {
            yield return handler.SendWebRequest();

            if(handler.error != null)
                Debug.LogError(handler.error);
            this.OnDatabaseActionCompleted?.Invoke(handler.downloadHandler.text);
        }
    }
}