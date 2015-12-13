using UnityEngine;
using System.Collections;

public class FirstLight : MonoBehaviour {

    public Vector3 distanceToObject;
    public Vector3 distanceToReach;
    public bool guide;
    public GameObject objectToGuide;
    private const float speed = 0.03f;
    private float initialInsensity;
    private const float maxIntensity = 8.0f;
    public delegate void DisturbAction();
    public static event DisturbAction OnDisturb;


    // Use this for initialization
    void Start () {
        guide = false;
        distanceToObject = Vector3.zero;
        initialInsensity = GetComponent<Light>().intensity;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        
        if(guide == true)
        {
            distanceToObject = transform.position - objectToGuide.transform.position;
            SetDistanceToObject();
            transform.position = objectToGuide.transform.position + distanceToObject;
            SetIntensity();
        }
        
	
	}

    void SetDistanceToObject()
    {
        distanceToObject.x = ComputeCoordinates(distanceToObject.x, distanceToReach.x);
        distanceToObject.y = ComputeCoordinates(distanceToObject.y, distanceToReach.y);
        distanceToObject.z = ComputeCoordinates(distanceToObject.x, distanceToReach.z);
    }
    float ComputeCoordinates(float origin, float target)
    {
        if (Mathf.Abs(target - origin) <= speed)
            return target;
        if ((target - origin) > 0)
            return origin + Mathf.Max(speed, (target - origin) * speed);
        return origin +  Mathf.Min(-speed, (target - origin) * speed);

        
    }
    void SetIntensity()
    {
        GetComponent<Light>().intensity = maxIntensity - ((distanceToReach.y - distanceToObject.y) / distanceToReach.y) * (maxIntensity - initialInsensity);

    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "hand")
        {
            OnDisturb();
        }
    }
}
