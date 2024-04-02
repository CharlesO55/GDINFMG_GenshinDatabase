using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] Button _refreshButton;
    [SerializeField] Button _loginButton;
    [SerializeField] TMP_InputField _adminIDField;
    [SerializeField] TMP_InputField _adminPasswordField;
    [SerializeField] AdminTools _adminTools;

    private void Start()
    {
        _adminTools.OnLoginSuccessful.AddListener(() =>
        {
            _adminTools.OnLoginSuccessful.RemoveAllListeners();
            this._refreshButton.gameObject.SetActive(true);
            this._loginButton.gameObject.SetActive(false);
            this._adminIDField.gameObject.SetActive(false);
            this._adminPasswordField.gameObject.SetActive(false);
        });
    }

    public void LoadToMainScreen(bool isRefreshMasterlist)
    {
        SceneLoader.Instance.LoadScene(EScenes.LOADING_SCREEN, new LoadingParams(EScenes.MAIN_SCREEN, isRefreshMasterlist, true, true, true));
    }
    


    public void CallLogin()
    {
        _adminIDField.text = TextCleaner.RegexAlphaNumeric(_adminIDField.text, "[^A-Za-z0-9]");
        _adminPasswordField.text = TextCleaner.RegexAlphaNumeric(_adminPasswordField.text, "[^A-Za-z0-9]");

        _adminTools.Login(_adminIDField.text, _adminPasswordField.text);
    }
}