using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateCharacter : MonoBehaviour
{



    void Start()
    {
        StartUpdateCharacter();   
    }

    void StartUpdateCharacter()
    {
        this.StartCoroutine(DoUpdateCharacter());
    }

    private IEnumerator DoUpdateCharacter()
    {
        WWWForm form = new WWWForm();
        FillForm(form);
        

        //using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/Update/UpdateCharacter.php", form)){
        using (UnityWebRequest handler = UnityWebRequest.Post(ConnectionSettings.SERVER_ADDRESS + "Update/UpdateCharacter.php", form)){
            yield return handler.SendWebRequest();

            if (handler.error == null)
            {
                Debug.Log("CHAR UPDATE: " + handler.downloadHandler.text);
            }
            else
            {
                Debug.LogError(handler.error);
                Debug.LogError(handler.downloadHandler.text);
            }
        }
    }

    void FillForm(WWWForm form)
    {
        form.AddField("FIELD_CharacrterName", "Test1");


        form.AddField("FIELD_Rarity", 5);
        form.AddField("FIELD_Region", "Liyue");
        form.AddField("FIELD_Vision", "Pyro");
        form.AddField("FIELD_Arkhe", "N/A");
        form.AddField("FIELD_Weapon", "N/A");
        form.AddField("FIELD_Model", "N/A");
        form.AddField("FIELD_Constellation", "N/A");
        form.AddField("FIELD_Birthday", "N/A");
        form.AddField("FIELD_Dish", "N/A");
        form.AddField("FIELD_Affiliation", "N/A");
        form.AddField("FIELD_Limited", "N/A");
        form.AddField("FIELD_Release", "2000-01-01");
        form.AddField("FIELD_Voice_en", "N/A");
        form.AddField("FIELD_Voice_cn", "N/A");
        form.AddField("FIELD_Voice_jp", "N/A");
        form.AddField("FIELD_Voice_kr", "N/A");
        form.AddField("FIELD_Ascension", "N/A");
        form.AddField("FIELD_Ascension_specialty", "N/A");
        form.AddField("FIELD_Ascension_material", "N/A");
        form.AddField("FIELD_Ascension_boss", "N/A");
        form.AddField("FIELD_Talent_material", "N/A");
        form.AddField("FIELD_Talent_book_1", "N/A");
        form.AddField("FIELD_Talent_book_2", "N/A");
        form.AddField("FIELD_Talent_book_3", "N/A");
        form.AddField("FIELD_Talent_book_4", "N/A");
        form.AddField("FIELD_Talent_book_5", "N/A");
        form.AddField("FIELD_Talent_book_6", "N/A");
        form.AddField("FIELD_Talent_book_7", "N/A");
        form.AddField("FIELD_Talent_book_8", "N/A");
        form.AddField("FIELD_Talent_book_9", "N/A");
        form.AddField("FIELD_Talent_book_10", "N/A");
        form.AddField("FIELD_Talent_weekly", "N/A");
        form.AddField("FIELD_hp_90_90", 0);
        form.AddField("FIELD_def_90_90", 0);
        form.AddField("FIELD_atk_90_90", 0);
        form.AddField("FIELD_hp_1_20", 0);
        form.AddField("FIELD_def_1_20", 0);
        form.AddField("FIELD_atk_1_20", 0);
        form.AddField("FIELD_special_0", "N/A");
        form.AddField("FIELD_special_1", "N/A");
        form.AddField("FIELD_special_2", "N/A");
        form.AddField("FIELD_special_3", "N/A");
        form.AddField("FIELD_special_4", "N/A");
        form.AddField("FIELD_special_5", "N/A");
        form.AddField("FIELD_special_6", "N/A");
        form.AddField("FIELD_character_description", "N/A");
        form.AddField("FIELD_total_banner_runs", 0);
        form.AddField("FIELD_last_banner_appearance", "0.0");
        form.AddField("FIELD_total_revenue", 0);
    }
}
