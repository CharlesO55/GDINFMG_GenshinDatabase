using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdminTools : MonoBehaviour
{
    [SerializeField] private Toggle m_AdminCheck;
    [SerializeField] private List<GameObject> m_AdminTools;

    private void Start()
    {
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
}
