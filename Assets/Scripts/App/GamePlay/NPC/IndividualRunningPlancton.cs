using UnityEngine;
using System.Collections;

public class IndividualRunningPlancton : MonoBehaviour, Core.StdInterfaces.Initiable
{


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
        if (state == states.Running)
            RunAwayMove();
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
    public void SetDisturbance(Vector3 disturbPoint)
    {
        //disturbance = dist;
        disturbingPoint = disturbPoint;
        state = states.Running;
    }
    public void Regroup()
    {
        initialPosition = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        state = states.Grouping;
    }

     void NormalMove()
    {
        transform.localPosition = GetPositionToCenter();
        if (transform.localPosition == initialPosition)
        {
            state = states.Normal;
            //disturbance = 0.0f;
        }
        //Debug.Log(this.name + "Normal Move" + transform.localPosition);
    }
    void RunAwayMove()
    {
        transform.Translate(GetTranslateMove(), Space.World);
        /*Vector3 safe = GetDestinationToRun();
        transform.position = transform.position +  safe * 0.01f;
        if (IsSafe(safe))
            state = states.Grouping;
*/
        //Debug.Log(this.name + "Run Move" + transform.localPosition);

    }
    /*bool IsSafe(Vector3 safePosition)
    {
        
        
        Vector3 distanceSafe = disturbingPoint - safePosition;
        Vector3 distanceSelf = disturbingPoint - transform.position;
        if (Mathf.Abs(distanceSafe.x) <= Mathf.Abs(distanceSelf.x) && Mathf.Abs(distanceSafe.y) <= Mathf.Abs(distanceSelf.y)  && Mathf.Abs(distanceSafe.z) <= Mathf.Abs(distanceSelf.z))
        {
            return true;
        }
        return false;
    }*/
    Vector3 GetTranslateMove()
    {
        Vector3 move;
        move = transform.position - disturbingPoint;

        move = move.normalized*speed;
        return move;
    }
    /*Vector3 GetDestinationToRun()
    {
        Vector3 runAway;
        runAway.x = ComputeDestinationCoordinates(disturbingPoint.x, transform.position.x);
        runAway.y = ComputeDestinationCoordinates(disturbingPoint.y, transform.position.y);
        runAway.z = ComputeDestinationCoordinates(disturbingPoint.z, transform.position.z);
        return runAway;
    }*/
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
    /*float ComputeDestinationCoordinates(float pointToFlee, float position)
    {

        if (pointToFlee > position)
            return pointToFlee - disturbance;
        return pointToFlee + disturbance;
    }*/
}
