using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public abstract class UIObject : MonoBehaviour
{

    public enum TouchState
    {
        None,
        Single,
        Double,
        Triple,
        Quadruple,
        ReSize,
        Move,
    }

    const string FingerTagName = "Finger";
    bool isSelect = false;
    private bool isTouchStay = false;
    private bool isTouchEnter = false;
    private bool isTouchExit = false;

    [SerializeField] private bool canMove = false;
    [SerializeField] protected bool isView = true;
    [SerializeField] private bool isCollision = true;
    protected Behaviour m_uiComponent;

    public bool isTouch
    {
        get
        {
            if (m_fingerObjects.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    private List<GameObject> m_fingerObjects = new List<GameObject>();

    protected void InitUIObject()
    {

        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    // Use this for initialization
    void Start()
    {

    }

    void Awake()
    {
    }

    public bool GetTouchEnter()
    {
        if (isCollision)
        {
            return isTouchExit;
        }
        else
        {
            return false;
        }
    }

    public bool GetTouchStay()
    {
        if (isCollision)
        {
            return isTouchStay;
        }
        else
        {
            return false;
        }
    }

    public bool GetTouchExit()
    {
        if (isCollision)
        {
            return isTouchExit;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        isTouchEnter = false;
        isTouchExit = false;
    }

    public void AddTransformPosition(Vector3 position)
    {

        if (canMove)
        {
            transform.localPosition += position;
        }

    }

    public void SetTransformPositon(Vector3 position)
    {

        if (canMove)
        {
            transform.localPosition = position;
        }

    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetIsCollisino(bool isCollision)
    {
        this.isCollision = isCollision;
    }

    protected abstract void SetUiComponent(Behaviour uiComponent);

    public void SetIsView(bool isView)
    {
        this.isView = isView;
        m_uiComponent.enabled = isView;
    }

    public void HideView()
    {
        m_uiComponent.enabled = false;
    }

    public void ReturnView()
    {
        m_uiComponent.enabled = this.isView;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == FingerTagName)
        {
            if (m_fingerObjects.Count == 0)
            {
                isTouchEnter = true;
            }
            m_fingerObjects.Add(other.gameObject);
            isTouchStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == FingerTagName)
        {
            m_fingerObjects.Remove(other.gameObject);
            if (m_fingerObjects.Count == 0)
            {
                isTouchExit = true;
                isTouchStay = false;
            }
        }
    }

}
