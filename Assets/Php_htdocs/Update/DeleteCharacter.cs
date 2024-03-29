using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class DeleteCharacter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UnityEngine.UI.Image buttonBody;
    public void OnPointerClick(PointerEventData eventData)
    {
        this.StartCoroutine(DeleteCurrentCharacter());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonBody.color = new Color32(224, 169, 0, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonBody.color = Color.red;
    }

    IEnumerator DeleteCurrentCharacter()
    {
        WWWForm form = new WWWForm();

        form.AddField("FIELD_Name", SelectCharacter.Instance.GeneralData.Character_name);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/DeleteCharacter.php", form))
        {
            yield return handler.SendWebRequest();

            if (handler.error != null)
                Debug.LogError("Failed to delete character" + handler.error);

            Debug.Log(handler.downloadHandler.text);

            RefreshViews.Instance.Refresh();
            SceneLoader.Instance.LoadScene("MainScreen");
        }
    }
}
