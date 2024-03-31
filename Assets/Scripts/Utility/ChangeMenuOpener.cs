using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuOpener : MonoBehaviour
{
    [SerializeField] private Animator m_BasicData;
    [SerializeField] private Animator m_AscensionData;

    public void ShowBasicData()
    {
        this.m_BasicData.SetBool("Open", true);
        this.m_AscensionData.SetBool("Open", false);
    }

    public void ShowAscensionData()
    {
        this.m_BasicData.SetBool("Open", false);
        this.m_AscensionData.SetBool("Open", true);
    }

    public void CloseAllPanels()
    {
        this.m_BasicData.SetBool("Open", false);
        this.m_AscensionData.SetBool("Open", false);
    }
}
