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

        string newCharName = "NewCharacter" + Random.Range(0, 1000000);
        form.AddField("FIELD_Name", newCharName);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/AddCharacter.php", form))
        {
            yield return handler.SendWebRequest();
        }
    }
}
