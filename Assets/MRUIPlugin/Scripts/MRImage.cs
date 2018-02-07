using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class MRImage : UIObject
{

    private Image m_image;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitUIObject();
        m_image = GetComponent<Image>();
        SetUiComponent(m_image);
    }

    protected override void SetUiComponent(Behaviour uiComponent)
    {
        m_uiComponent = uiComponent;
    }

}
