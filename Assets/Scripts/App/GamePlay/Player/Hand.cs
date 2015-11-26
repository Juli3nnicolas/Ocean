using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	
	public delegate void StartScene();
	public static event StartScene OnStart;


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

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "removable")
		{
			if(other.name == "FirstLight")
			{
				Destroy(other.gameObject);
				OnStart();
				//transform.parent.gameObject.GetComponent<App.Gameplay.MovePlayer>().init(); //! Must be moved somewhere else
			}
		}
	}
}
