using UnityEngine;
using System.Collections;

public class IndividualPlancton : MonoBehaviour {

	private float movementSize;
	// Use this for initialization
	void Start () {
	
		movementSize = Random.Range (1.0f, 3.0f);

	}

	public void Move (float movementAmplitude)
	{
		
		Vector3 movement = new Vector3 (Random.Range (0.01f, movementAmplitude), Random.Range (0.01f, movementAmplitude), Random.Range (0.01f, movementAmplitude)) * movementSize;
		transform.localPosition = transform.localPosition + movement;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeColor(Color newColor )
	{
		this.GetComponent<Light> ().color = newColor;
	}
}
