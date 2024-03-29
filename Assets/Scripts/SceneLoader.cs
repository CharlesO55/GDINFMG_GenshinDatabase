using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{ 
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string name)
    {
        Debug.Log($"Loading scene:{name}");
        SceneManager.LoadScene(name);
    }
}
