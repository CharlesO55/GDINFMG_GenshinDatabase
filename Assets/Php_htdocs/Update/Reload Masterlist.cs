using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ReloadMasterlist : MonoBehaviour
{
    public static ReloadMasterlist Instance;
    public UnityEvent<string> OnDatabaseActionCompleted;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void CallReloadMasterlist()
    {
        this.StartCoroutine(ReloadMasterlistTable());  
    }


    private IEnumerator ReloadMasterlistTable()
    {
        Debug.LogWarning("Restoring masterlist. This will delete all changes made.");

        using (UnityWebRequest handler = UnityWebRequest.Get("http://localhost/Update/ReloadMasterlist.php"))
        {
            yield return handler.SendWebRequest();

            if(handler.error != null)
            {
                Debug.LogError("Failed to restore masterlist " + handler.error);
            }

            this.OnDatabaseActionCompleted.Invoke(handler.downloadHandler.text);
        }
    }
}
