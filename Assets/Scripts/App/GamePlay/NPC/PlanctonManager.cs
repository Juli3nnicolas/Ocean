using UnityEngine;
using System.Collections;

public class PlanctonManager : MonoBehaviour {

	public const int influenceZone = 10;
	private GameObject rightHand;

	//How much the player has interacted with plancton
	private float disturbance;

	// Use this for initialization
	void Start () {

		InputManager.OnMove += Move;
		rightHand = GameObject.Find ("RightHand");
		disturbance = 0.0f;
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
			Vector3 movement=Vector3.zero;
			Vector3 handMovement = InputManager.Instance.GetRightHandMovement ();
			disturbance += handMovement.x+handMovement.y+handMovement.z;

			movement.x= handMovement.y*Mathf.Pow((influenceZone-distance),2)*(Mathf.Exp(-Mathf.Pow((disturbance-4)/3, 2)))*Random.Range(0.2f, 8.0f);
			
			movement.z= handMovement.z*Mathf.Pow((influenceZone-distance),2)*(Mathf.Exp(-Mathf.Pow((disturbance-4)/3, 2)))*Random.Range(0.2f, 8.0f);
			transform.Translate(movement);
			if(InputManager.Instance.HasMovementChanged())
			{
				foreach(IndividualPlancton p in this.GetComponentsInChildren<IndividualPlancton>())
				{
					p.Move();
				}
			}
		}
	}
	public void ChangeColor(Color newColor)
	{
		foreach(IndividualPlancton p in this.GetComponentsInChildren<IndividualPlancton>())
		{
			p.ChangeColor(newColor);
		}
	}

}
