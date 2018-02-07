using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MRUIManager : MonoBehaviour
{

    [SerializeField] private GameObject m_headSet;
    [SerializeField] private float m_followingSpeed = 0.9f;
    [SerializeField] private float m_positionThreshold = 0.5f;
    [SerializeField] private float m_angleThreshold = 30f;
    [SerializeField] private float m_uiReferenceDistance = 0.5f;
    [SerializeField] private float m_uiReferenceXAngle = 45f;

    private List<UIObject> m_uiObjects = new List<UIObject>();

    private static MRUIManager ins;

    public static MRUIManager Instance
    {
        get
        {
            if (ins == null)
            {
                ins = (MRUIManager)GameObject.FindObjectOfType(typeof(MRUIManager));
            }
            return ins;
        }
    }

    private Vector3 m_headSetPosition
    {
        get
        {
            return m_headSet.transform.position;
        }
    }

    private float m_headSetYAngle
    {
        get
        {
            return m_headSet.transform.localEulerAngles.y;
        }
    }

    private Vector3 m_currentCenterPosition;
    private float m_currentCenterYAngle;
    private Vector3 m_targetCenterPostion;
    private float m_targetCenterYAngle;

    private float m_followFinishDistance = 0.01f;
    private float m_followFinishAngle = 1;
    private bool m_isInitilize = false;
    private bool m_isFollowing = false;

    void Initialize()
    {

        if (m_headSet != null)
        {
            m_targetCenterPostion = m_headSetPosition;
            m_targetCenterYAngle = m_headSetYAngle;
            m_isInitilize = true;
            m_isFollowing = true;
        }
    }

    private void UIFollowing()
    {


        SetUIPosition(
            Vector3.Slerp(m_headSetPosition, m_currentCenterPosition, m_followingSpeed),
            Mathf.LerpAngle(m_headSetYAngle, m_currentCenterYAngle, m_followingSpeed));

        CheckFollowFinished();

    }

    private void CheckFollowFinished()
    {

        if (Vector3.Distance(m_currentCenterPosition, m_headSetPosition) <= m_followFinishDistance &&
           Mathf.Abs(m_headSetYAngle - m_currentCenterYAngle) <= m_followFinishAngle)
        {
            m_isFollowing = false;
        }

    }

    private void CheckIsFollowing()
    {

        if (Vector3.Distance(m_targetCenterPostion, m_headSetPosition) >= m_positionThreshold)
        {
            m_isFollowing = true;
            SetCenterValues();
            return;
        }
        if (Mathf.Abs(m_targetCenterYAngle - m_headSetYAngle) >= m_angleThreshold)
        {
            SetCenterValues();
            m_isFollowing = true;
            return;
        }

        m_isFollowing = false;
        return;
    }

    void SetCenterValues()
    {
        m_targetCenterPostion = m_headSetPosition;
        m_targetCenterYAngle = m_headSetYAngle;
    }

    void SetUIPosition(Vector3 targetCenterPosition, float targetYAngle)
    {

        Quaternion uiAngle = Quaternion.AngleAxis(targetYAngle, Vector3.up) * Quaternion.AngleAxis(m_uiReferenceXAngle, Vector3.right);
        Vector3 forward = uiAngle * Vector3.forward;

        m_currentCenterPosition = targetCenterPosition;
        m_currentCenterYAngle = targetYAngle;

        transform.rotation = uiAngle * Quaternion.AngleAxis(180, Vector3.up);
        transform.position = targetCenterPosition + forward * m_uiReferenceDistance;

    }

    public static void RemoveAtNull<T>(List<T> list)
    {
        list.RemoveAll(MatchNullable);
    }

    private static bool MatchNullable<T>(T t){
        return (t == null) ? true : false;
    }

    private void RemoveAtNullUiObjects()
    {
        RemoveAtNull(m_uiObjects);
    }

    public void HideUIView()
    {
        RemoveAtNullUiObjects();
        foreach (UIObject uiObject in m_uiObjects)
        {
            uiObject.HideView(); ;
        }
    }

    public void ReturnUIView()
    {
        RemoveAtNullUiObjects();
        foreach (UIObject uiObject in m_uiObjects)
        {
            uiObject.ReturnView();
        }
    }

    void Update()
    {

        if (m_isInitilize)
        {
            if (m_isFollowing)
            {
                UIFollowing();
            }
            else
            {
                CheckIsFollowing();
            }

        }
        else
        {
            Initialize();
        }

    }
}
