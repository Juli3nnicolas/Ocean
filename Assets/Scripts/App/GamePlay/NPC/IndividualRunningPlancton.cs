using UnityEngine;
using System.Collections;

public class IndividualRunningPlancton : MonoBehaviour, Core.StdInterfaces.Initiable
{
	/**
	 * Planctons have three states : nomal (they don't move),
	 * running (they run away without carring of the group)
	 * and grouping (they return to their initial position in the group)
	 */

    public enum states { Normal, Running, Grouping };

    Vector3 initialPosition;
    public Vector3 disturbingPoint;
    //public float disturbance;
    float speed;
    public states state;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//If he is running
        if (state == states.Running)
            RunAwayMove();
		//If he is grouping
        else
        {
            if(state == states.Grouping)
                NormalMove();
        }
            
	}

    public void Init()
    {
        initialPosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        transform.localPosition = initialPosition;
        speed = Random.Range(0.02f, 0.1f);
        state = states.Normal;
    }
    public void Terminate()
    {
        Destroy(this);
    }
	//Something has disturbed the plancton, we tell him to run and set the origin of the disturbance
    public void SetDisturbance(Vector3 disturbPoint)
    {
        disturbingPoint = disturbPoint;
        state = states.Running;
    }
    public void Regroup()
    {
        initialPosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        state = states.Grouping;
    }
	/*
	 * The normal move : if the plancton is not in his initial place relative to the group
	 * he try to reach it
	 */
     void NormalMove()
    {
        transform.localPosition = GetPositionToCenter();
		//If he has reached his initial position, he turn his state to normal
        if (transform.localPosition == initialPosition)
        {
            state = states.Normal;
        }
    }
    void RunAwayMove()
    {
        transform.Translate(GetTranslateMove(), Space.World);
    }
    Vector3 GetTranslateMove()
    {
        Vector3 move;
        move = transform.position - disturbingPoint;

        move = move.normalized*speed;
        return move;
    }
    Vector3 GetPositionToCenter()
    {
        Vector3 positionToCenter;
        positionToCenter.x = ComputeCoordinates(transform.localPosition.x, initialPosition.x);
        positionToCenter.y = ComputeCoordinates(transform.localPosition.y, initialPosition.y);
        positionToCenter.z = ComputeCoordinates(transform.localPosition.z, initialPosition.z);
        return positionToCenter;
    }
    float ComputeCoordinates(float origin, float target)
    {
        if (Mathf.Abs(target - origin) <= speed)
            return target;
        if ((target - origin) > 0)
        {
            return origin + Mathf.Max(speed, (target - origin) * speed);
        }
        return origin + Mathf.Min(-speed, (target - origin) * speed);
    }

    public bool IsInPlace()
    {
        if (state == states.Normal)
            return true;
        return false;
    }
}
