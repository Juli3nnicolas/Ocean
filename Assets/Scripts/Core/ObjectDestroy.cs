using UnityEngine;
using System.Collections;

public class ObjectDestroy {

    GameObject objectToDestroy;
    float step;
    
    public ObjectDestroy(GameObject ob, float s)
    {
        objectToDestroy = ob;
        step = s;
    }
    public float GetStep()
    {
        return step;
    }
    public GameObject GetObject()
    {
        return objectToDestroy;
    }
}
