using UnityEngine;
using System.Collections;


public class InputManager : MonoBehaviour {

	private Vector3 rightHandPosition;
	private Vector3 leftHandPosition;
	private Vector3 rightHandMovement;
	private Vector3 leftHandMovement;
	public delegate void MoveAction();
	public static event MoveAction OnMove;
	private static InputManager instance;

	private bool movementChange;

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
		rightHandPosition = new Vector3(-2,0 , -1);
		leftHandPosition = new Vector3(-2, 0, 1);
		rightHandMovement = Vector3.zero;
		leftHandMovement = Vector3.zero;
		OnMove ();

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MakeMove(Vector3 rightMovement, Vector3 leftMovement)
	{
		movementChange=true;
		if (rightMovement == rightHandMovement && leftMovement == leftHandMovement) {
			movementChange = false;
		}

		if (Mathf.Abs (rightHandPosition.x + rightMovement.x) > 3)
			rightMovement.x = 0;
		if (Mathf.Abs (rightHandPosition.y + rightMovement.y) > 2)
			rightMovement.y = 0;
		if (Mathf.Abs (rightHandPosition.z + rightMovement.z) > 2)
			rightMovement.z = 0;
		Debug.Log (Mathf.Abs (rightHandPosition.y + rightMovement.y));


		rightHandMovement = rightMovement;
		rightHandPosition += rightMovement;


		leftHandMovement = leftMovement;
		leftHandPosition += leftMovement;
		OnMove ();

	}

	public Vector3 GetRightHandPosition ()
	{
		return this.rightHandPosition;
	}

	public Vector3 GetRightHandMovement ()
	{
		return this.rightHandMovement;
	}

	public bool HasMovementChanged()
	{
		return movementChange;
	}
	

}
