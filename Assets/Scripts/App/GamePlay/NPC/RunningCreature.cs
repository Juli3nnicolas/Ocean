using UnityEngine;
using System.Collections;

public class RunningCreature : MonoBehaviour, Core.StdInterfaces.Initiable
{
	/*
	 * A creature that flee whe the player approach
	 */
    private GameObject player;
    private float distanceToRun;
    private Vector3 pointToReach;
    private float speed;
    private const float tol = 0.1f;
	// Use this for initialization
	void Start () {
    }
	

    public void Init()
    {

        player = GameObject.Find("Player");
        pointToReach = transform.position;
        speed = Random.Range(0.001f, 0.05f);
        distanceToRun = Random.Range(10.0f, 15.0f);
	}
	// If the player is close, we compute where to run
	void Update () {

		float distance = Vector3.Distance(player.transform.position, transform.position);
		if (distance < distanceToRun)
		{
			if(pointToReach == transform.position)
				SetDestination(distance);
			Move(distance);
		}
	}
    public void Terminate()
    {
        Destroy(this.gameObject);
    }
    void SetDestination(float distance)
    {
            if (player.transform.position.x - transform.position.x > 0)
                pointToReach.x = transform.position.x-distanceToRun;
            else
                pointToReach.x = transform.position.x+distanceToRun;
            if (player.transform.position.z - transform.position.z > 0)
                pointToReach.z = transform.position.z - distanceToRun;
            else
                pointToReach.z = transform.position.z +distanceToRun;
            
    }
	//If the creature is oriented in the good direction to run, it moves
	//Else it rotate
    void Move(float distance)
    {
        Vector3 targetDir =pointToReach - transform.position;
        float angle = Vector3.Angle(transform.forward, targetDir);
        if (angle < 5.0F)
            transform.position = new Vector3(ComputeDestination(pointToReach.x, transform.position.x, distance), transform.position.y, ComputeDestination(pointToReach.z, transform.position.z, distance));
        else
        {
            transform.Rotate(0.0f, 5.0f, 0.0f);
        }
    }
    float ComputeDestination(float toReach, float position, float distance)
    {
        if (Mathf.Abs(toReach - position) < tol)
            return toReach;
        if (toReach > position)
                return position - (-Mathf.Log(distance) + 1) * speed;
        return position + (-Mathf.Log(distance) + 1) * speed;
    }
}
