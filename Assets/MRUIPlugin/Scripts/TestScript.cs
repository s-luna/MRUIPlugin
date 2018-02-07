using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{

    [SerializeField] private MRImage m_mrImage;

    // Use this for initialization
    void Start()
    {
        m_mrImage.SetIsView(false);
        List<string> stlist = new List<string>();
        stlist.Add("a");
        stlist.Add(null);
        stlist.Add("b");
        stlist.Add(null);
        stlist.Add("c");
        MRUIManager.RemoveAtNull(stlist);
        foreach (string st in stlist)
        {
            Debug.Log(st);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            m_mrImage.SetIsView(false);
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            m_mrImage.SetIsView(true);
        }
    }
}