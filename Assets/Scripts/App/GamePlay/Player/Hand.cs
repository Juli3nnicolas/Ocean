using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPosition(Vector3 newPosition)
	{
		this.transform.localPosition = newPosition;
	}

	public void SetMove (Vector3 move)
	{
		this.transform.Translate (move, Space.World);
	}

}
