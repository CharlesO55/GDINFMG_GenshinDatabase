using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AdminTools : MonoBehaviour
{
    //public static AdminTools Instance;
    public UnityEvent OnLoginSuccessful;

    bool _isLoggedIn = false;

    [SerializeField] private Toggle m_AdminCheck;
    [SerializeField] private List<GameObject> m_AdminTools;


    /*private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }*/


    private void Start()
    {
        Debug.Log(ConnectionSettings.SERVER_ADDRESS + "Update/CreateAdmins.php");
        this.StartCoroutine(CreateAdminTable());

        this.StartCoroutine(this.SearchAdminToggle());
    }




    private IEnumerator SearchAdminToggle()
    {
        while (!this.m_AdminCheck)
        {
            // Set to find any object as admin check is the only toggle UI
            this.m_AdminCheck = GameObject.FindAnyObjectByType<Toggle>();

            yield return null;
        }
    }
    public void ToggleAdminTools()
    {
        foreach (GameObject tool in this.m_AdminTools)
            tool.SetActive(this.m_AdminCheck.isOn);
    }






    IEnumerator CreateAdminTable()
    {
        //using (UnityWebRequest handler = UnityWebRequest.Get("http://localhost/Update/CreateAdmins.php"))
        using (UnityWebRequest handler = UnityWebRequest.Get(ConnectionSettings.SERVER_ADDRESS + "Update/CreateAdmins.php"))
        {
            yield return handler.SendWebRequest();

            if (handler.error != null)
            {
                Debug.LogError(handler.error);
            }
            Debug.Log(handler.downloadHandler.text);
        }
    }

    public void Login(string ID, string password)
    {
        this.StartCoroutine(VerifyPassword(ID, password));
    }

    private IEnumerator VerifyPassword(string ID, string password)
    {
        WWWForm form = new();

        
        form.AddField("FIELD_ID", ID);
        form.AddField("FIELD_Password", password);


        //using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Retrieve/AdminLogin.php", form))
        using (UnityWebRequest handler = UnityWebRequest.Post(ConnectionSettings.SERVER_ADDRESS + "Retrieve/AdminLogin.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.isHttpError != null)
            {
                Debug.LogError(handler.error);
            }

            Debug.Log($"Login was {handler.downloadHandler.text}");
            if (handler.downloadHandler.text.Contains("SUCCESS"))
            {
                this._isLoggedIn = true;
                this.OnLoginSuccessful?.Invoke();
            }
        }
    }
}
