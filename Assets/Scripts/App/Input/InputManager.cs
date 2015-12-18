using UnityEngine;
using System.Collections;


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

	public void MakeMoveHand(Vector3 rightMovement, Vector3 leftMovement)
	{
        
		//Limit Hand moves
		/*if (Mathf.Abs (rightHand.GetPosition().x + rightMovement.x) > 3)
			rightMovement.x = 0;
		if (Mathf.Abs (rightHand.GetPosition().y + rightMovement.y) > 2)
			rightMovement.y = 0;
		if (Mathf.Abs (rightHand.GetPosition().z + rightMovement.z) > 2)
			rightMovement.z = 0;
        if (Mathf.Abs(leftHand.GetPosition().x + leftMovement.x) > 3)
            leftMovement.x = 0;
        if (Mathf.Abs(leftHand.GetPosition().y + leftMovement.y) > 2)
            leftMovement.y = 0;
        if (Mathf.Abs(leftHand.GetPosition().z + leftMovement.z) > 2)
            leftMovement.z = 0;*/
        
        rightHand.SetPosition(rightHand.GetPosition()+rightMovement);
        leftHand.SetPosition(leftHand.GetPosition() + leftMovement);
        

	}
    public void ChangePositionHand(Vector3 rightPosition, Vector3 leftPosition)
    {
        rightHand.SetPosition(rightPosition);
        leftHand.SetPosition(leftPosition);

    }

    
	

}
