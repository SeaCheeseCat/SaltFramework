using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class UIToggleView : MonoBehaviour
{
    private Toggle m_toggle;
    public GameObject NoCheck;
    private void Awake()
    {
        m_toggle = GetComponent<Toggle>();
        m_toggle.onValueChanged.AddListener(ison=> {

            NoCheck.SetActive(!ison);
        });
    }
}
