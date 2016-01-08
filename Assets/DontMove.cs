using UnityEngine;
using System.Collections;

public class DontMove : MonoBehaviour {

    void Awake()
    {
        m_firstPos = transform;
    }

    void LateUpdate()
    {
        transform.position = m_firstPos.position;
        transform.rotation = m_firstPos.rotation;
    }

    private Transform m_firstPos;
}
