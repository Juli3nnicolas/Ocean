using UnityEngine;
using System.Collections;

public class Scene1 : MonoBehaviour {


	private GameObject player;
	
	// Use this for initialization
	void Start () {

		player= GameObject.Find ("Player");
		Hand.OnStart += Init;

	}
	
	// Update is called once per frame
	void Update () {

		if (player.transform.position.y > 100)
			Application.Quit ();
		
	}

	void Init()
	{
		StartMusic ();
		StartPlayerMovement ();
	}

	void StartMusic()
	{

	}
	void StartPlayerMovement()
	{
		player.GetComponent<App.Gameplay.MovePlayer> ().init ();
	}


}
