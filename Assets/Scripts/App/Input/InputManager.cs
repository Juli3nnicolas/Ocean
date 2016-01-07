using UnityEngine;
using System.Collections;

/** This class is the generic Input Manager
 * All input managers specific to one harware use it
 */

public class InputManager : MonoBehaviour {

	private Vector3 rightHandPosition;
	private Vector3 leftHandPosition;
	private Vector3 rightHandMovement;
	private Vector3 leftHandMovement;

    public Hand rightHand;
    public Hand leftHand;
	public Player player;
	private static InputManager instance;
    
	public InputManager ()
	{
		instance = this;
	}
	public static InputManager Instance
	{
		get { return instance ?? (instance = new GameObject ("InputManager").AddComponent<InputManager> ());}
	}
	

	// Use this for initialization
	void Start () {
        rightHand = GameObject.Find("RightHand").GetComponent<Hand>();
		leftHand = GameObject.Find("LeftHand").GetComponent<Hand>();


    }
	
	// Update is called once per frame
	void Update () {
	
	}
	/*
	 * Make the hands move
	 */
	public void MakeMoveHand(Vector3 rightMovement, Vector3 leftMovement)
	{   
        rightHand.SetPosition(rightHand.GetPosition()+rightMovement);
        leftHand.SetPosition(leftHand.GetPosition() + leftMovement);
        

	}
	/*
	 * Set the hand position
	 */
    public void ChangePositionHand(Vector3 rightPosition, Vector3 leftPosition)
    {
        rightHand.SetPosition(rightPosition);
        leftHand.SetPosition(leftPosition);

    }

    
	

}
