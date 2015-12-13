using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

    private Vector3 position;
    private Vector3 previousPosition;
    private Vector3 move;
    private Vector3 previousMove;
    private Vector3 speed;
    private Vector3 previousSpeed;
    private Vector3 acceleration;
    private Vector3 previousAcceleration;

    


    // Use this for initialization
    void Start () {
	    
	}
	
    public void Init(Vector3 position)
    {
        transform.localPosition = position;
        position = transform.localPosition;
        previousPosition = position;
        move = Vector3.zero;
        previousMove = move;
        speed = Vector3.zero;
        previousSpeed = speed;
        acceleration = Vector3.zero;
        previousAcceleration = acceleration;
    }
	// Update is called once per frame
	void Update () {
        position = transform.localPosition;
        move = position - previousPosition;
        speed = move / Time.deltaTime;
        acceleration = (speed - previousSpeed) / Time.deltaTime;

        previousPosition = position;
        previousMove = move;
        previousSpeed = speed;
        previousAcceleration = acceleration;
	}

	public void SetPosition(Vector3 newPosition)
	{
        transform.localPosition = newPosition;
	}
    
    public Vector3 GetPosition()
    {
        return position;
    }
    public Vector3 GetMove()
    {
        return move;
    }
    public bool HasSpeedChanged()
    {
        return speed != Vector3.zero;
    }
}
