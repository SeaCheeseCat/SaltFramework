using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIModelBG : MonoBehaviour
{
    public Image image;

    private UIBase m_ui;
    private bool m_clickClose = false;
    public void InitBG (UIBase ui, bool clickClose)
    {
        m_ui = ui;
        m_clickClose = clickClose;

        RectTransform rect = transform as RectTransform;
        rect.offsetMin = new Vector2 (0, 0);
        rect.offsetMax = new Vector2 (0, 0);
    }

    public void OnClick ()
    {
        if (m_clickClose && m_ui != null)
        {
            m_ui.Close ();
        }
    }

    private void Start ()
    {
        image.DOFade (0.25f, 0.4f);
    }
}