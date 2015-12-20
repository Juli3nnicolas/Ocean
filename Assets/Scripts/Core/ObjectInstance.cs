using UnityEngine;
using System.Collections;

public class ObjectInstance {

    GameObject objectType;
    Vector3 position;
    float step;
    float destroyStep;

    public ObjectInstance(GameObject t, Vector3 p, float s, float dS)
    {
        objectType = t;
        position = p;
        step = s;
        destroyStep = dS;
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
    public float GetDestroyStep()
    {
        return destroyStep;
    }

}
