using UnityEngine;
using System.Collections;

public class ObjectInstance {

    GameObject objectType;
    Vector3 position;
    float step;

    public ObjectInstance(GameObject t, Vector3 p, float s)
    {
        objectType = t;
        position = p;
        step = s;
    }
    public float GetStep()
    {
        return step;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public GameObject GetObjectType()
    {
        return objectType;
    }

}
