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
    public event System.EventHandler OnHandMove;




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

        System.EventHandler handler = OnHandMove;
        if (handler != null && (position != previousPosition || move != previousMove || speed != previousSpeed || acceleration != previousAcceleration))
        {
            OnHandMove(this, System.EventArgs.Empty);
        }

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
    public Vector3 GetSpeed()
    {
        return speed;
    }
    public Vector3 GetAcceleration()
    {
        return acceleration;
    }
    public bool HasSpeedChanged()
    {
        return speed != Vector3.zero;
    }
}
