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

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "removable")
		{
			if(other.name == "FirstLight")
			{
				this.transform.parent.GetComponent<Rigidbody>().AddForce(Vector3.up*50);
				Destroy(other.gameObject);
				transform.parent.gameObject.GetComponent<App.Gameplay.MovePlayer>().init(); //! Must be moved somewhere else
			}
		}
	}
}
