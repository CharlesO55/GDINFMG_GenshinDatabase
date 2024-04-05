using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class AddCharacter : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        this.StartCoroutine(InsertNewCharacter());
    }

    IEnumerator InsertNewCharacter()
    {
        WWWForm form = new WWWForm();

        string newCharName = "_" + System.DateTime.Now.ToString("HH:mm:ss tt");
        form.AddField("FIELD_Name", newCharName);

        //using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/AddCharacter.php", form))
        using (UnityWebRequest handler = UnityWebRequest.Post(ConnectionSettings.SERVER_ADDRESS + "Update/AddCharacter.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error != null)
                Debug.LogError("Failed to insert new character" + handler.error);

            Debug.Log(handler.downloadHandler.text);

            if (FilterCharacters.Instance) 
                FilterCharacters.Instance.CallFilterByMultiple();
        }
    }
}
