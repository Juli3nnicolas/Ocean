using UnityEngine;
using System.Collections;

public class deltaPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {

        m_position = transform.position;
        m_previousPos = m_position;
	}
	
	// Update is called once per frame
	void Update () {
        m_previousPos = m_position;
        m_position = transform.position;
	}

    public Vector3 getDeltaPos()
    {
        Vector3 delta = m_position - m_previousPos;

        return delta;
    }

    Vector3 m_position;
    Vector3 m_previousPos;
}
