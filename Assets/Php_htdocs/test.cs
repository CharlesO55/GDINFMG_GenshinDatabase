using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
    private void Awake()
    {
        this.StartCoroutine(StartTest());
    }
    private IEnumerator StartTest()
    {
        string url = "http://localhost/filterCharacters.php";
        WWW request = new WWW(url);
        yield return request;


        Debug.Log(request.text);
        string[] messages = request.text.Split("\t");
    }
}
