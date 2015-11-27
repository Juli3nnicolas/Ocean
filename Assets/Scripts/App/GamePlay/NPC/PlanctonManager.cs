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
			transform.Translate(ComputeMoves(distance));
			if(InputManager.Instance.HasMovementChanged())
			{
				foreach(IndividualPlancton p in this.GetComponentsInChildren<IndividualPlancton>())
				{
					p.Move();
				}
			}
			ChangeColor(new Color((1- this.disturbance/10.0f), disturbance/10.0f , Mathf.Log(disturbance)));
		}
	}
	public void ChangeColor(Color newColor)
	{
		foreach(IndividualPlancton p in this.GetComponentsInChildren<IndividualPlancton>())
		{
			p.ChangeColor(newColor);
		}
	}
	Vector3 ComputeMoves(float distance)
	{
		Vector3 movement=Vector3.zero;
		Vector3 handMovement = InputManager.Instance.GetRightHandMovement ();
		if(disturbance<10-(Mathf.Abs(handMovement.x+handMovement.y+handMovement.z))*2)
			disturbance += Mathf.Abs(handMovement.x+handMovement.y+handMovement.z)*2;
		movement.x= handMovement.y*Mathf.Pow((influenceZone-distance),2)*(Mathf.Exp(-Mathf.Pow((disturbance-4)/3, 2)))*Random.Range(0.2f, 8.0f);
		
		movement.z= handMovement.z*Mathf.Pow((influenceZone-distance),2)*(Mathf.Exp(-Mathf.Pow((disturbance-4)/3, 2)))*Random.Range(0.2f, 8.0f);

		return movement;

	}

}
