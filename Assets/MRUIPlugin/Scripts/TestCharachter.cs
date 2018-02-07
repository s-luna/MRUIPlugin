using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharachter : MonoBehaviour {

    [SerializeField] private float m_moveSpeed = 0.1f;
    private float m_deltaSpeed{
        get{
            return m_moveSpeed * Time.deltaTime;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	 
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.W)){
            transform.position += Vector3.forward * m_deltaSpeed;
        }
        if(Input.GetKey(KeyCode.S)){
            transform.position += Vector3.back * m_deltaSpeed;
        }
        if(Input.GetKey(KeyCode.D)){
            transform.position += Vector3.right * m_deltaSpeed;
        }
        if(Input.GetKey(KeyCode.A)){
            transform.position += Vector3.left * m_deltaSpeed;
        }
        if(Input.GetKey(KeyCode.Space)){
            transform.position += Vector3.up * m_deltaSpeed;
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            transform.position += Vector3.down * m_deltaSpeed;
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(0,100*m_deltaSpeed,0);
        }
        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(0,-100*m_deltaSpeed,0);
        }
	}
}
