using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Hand rightHand;
	
	// Use this for initialization
	void Start () {
		rightHand = this.GetComponentsInChildren<Hand> ()[0];
		rightHand.SetPosition(InputManager.Instance.GetRightHandPosition ());
	
		InputManager.OnMove += Move;

	}

	// Update is called once per frame
	void Update () {
		
	}

	void Move ()
	{
		rightHand.SetPosition (InputManager.Instance.GetRightHandPosition ());
	}
}
