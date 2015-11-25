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
	

}
