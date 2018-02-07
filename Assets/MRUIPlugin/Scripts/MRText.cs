using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMesh))]

public class MRText : UIObject
{

    private Text m_textMesh;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitUIObject();
        m_textMesh = GetComponent<Text>();
        SetUiComponent(m_textMesh);
    }

    protected override void SetUiComponent(Behaviour uiComponent)
    {
        m_uiComponent = uiComponent;
    }

}
