using UnityEngine;
using System.Collections;

public class Scene1 : MonoBehaviour {


	private GameObject player;
	
	// Use this for initialization
	void Start () {

		player= GameObject.Find ("Player");
		Hand.OnStart += StartPlayerMovement;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void StartPlayerMovement()
	{
		player.GetComponent<App.Gameplay.MovePlayer> ().init ();
	}


}
