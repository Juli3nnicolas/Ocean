using UnityEngine;
using System.Collections;

public class ObjectInstance {

	/*
	 * This class regroup a prefab and information about his instatiation
	 * 
	 */

	//The prefab
    GameObject objectType;
    Vector3 position; //Where it shoulb be instatiated
    float step; //Which height the player must have reached before we instatiate the object
    float destroyStep; //Which height the player must have reached before we destroy the object

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
