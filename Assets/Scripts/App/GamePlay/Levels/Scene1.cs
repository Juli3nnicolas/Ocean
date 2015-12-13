﻿using UnityEngine;
using System.Collections;

public class Scene1 : MonoBehaviour {


	private GameObject player;
    private GameObject firstLight;
    private bool started;
	
	// Use this for initialization
	void Start () {

		player= GameObject.Find ("Player");
        firstLight = GameObject.Find("FirstLight");
        FirstLight.OnDisturb += Init;
        started = false;

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
        FirstLight.OnDisturb += Init;
	}

	void StartMusic()
	{
		GetComponent<AudioSource>().Play();
	}
	
	void StartPlayerMovement()
	{
        if (!started)
        {
            started = true;
            player.GetComponent<App.Gameplay.MovePlayer>().init();
            firstLight.GetComponent<Animator>().enabled = false;
            firstLight.GetComponent<Light>().range = 0.5f;
            firstLight.GetComponent<FirstLight>().objectToGuide = player;
            firstLight.GetComponent<FirstLight>().distanceToReach = new Vector3(0.0f, 10.0f, 0.0f);
            firstLight.GetComponent<FirstLight>().guide = true;
        }
	}


}
