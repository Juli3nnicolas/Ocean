using UnityEngine;
using System.Collections;

public class PlanctonManager : MonoBehaviour {

	public const int influenceZone = 10;
	// Use this for initialization
	void Start () {

		InputManager.OnMove += Move;
		rightHand = GameObject.Find ("RightHand");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Move()
	{
		//If the hand is close
		float distance = Vector3.Distance (this.transform.position, rightHand.transform.position );
		if ( distance < influenceZone)
		{
			transform.position = transform.position + InputManager.Instance.GetRightHandMovement ()*(influenceZone-distance);
		}
	}
	
	private GameObject rightHand;
}
