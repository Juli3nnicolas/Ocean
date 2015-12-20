using UnityEngine;
using System.Collections;

public class Scene1 : MonoBehaviour {


	private GameObject player;
    private GameObject firstLight;
    private bool started;

    //Count to manage objects of first scene
    private System.Collections.Generic.Queue<ObjectInstance> objectsToPlace;

    //Prefabs
    public GameObject runningPlanctonPrefab;
    public GameObject planctonPrefab;
    public GameObject creaturePrefab;
    public GameObject loupiottePrefab;
	
	// Use this for initialization
	void Start () {

		player= GameObject.Find ("Player");
        firstLight = GameObject.Find("FirstLight");
        FirstLight.OnDisturb += Init;
        InitObjectsToPlace();
        started = false;

	}
	
	// Update is called once per frame
	void Update () {


        Place();
		
	}
    void Place()
    {
        while(objectsToPlace.Count > 0 &&  player.transform.position.y >= objectsToPlace.Peek().GetStep()) 
        {
            ObjectInstance instanceToCreate = objectsToPlace.Dequeue();
            GameObject newObject = (GameObject) Instantiate(instanceToCreate.GetObjectType(), instanceToCreate.GetPosition(), Quaternion.identity);
            newObject.GetComponent<Core.StdInterfaces.Initiable>().Init();

        }

    }
    void InitObjectsToPlace()
    {
        objectsToPlace = new System.Collections.Generic.Queue<ObjectInstance>();

        //We had instances of the objects we want to instantiate ordered by the height they must appear
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(5.0f, 20.0f, 2.0f), 0.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(-3.0f, 25.0f, 0.0f), 0.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 30.0f, 1.0f), 0.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 30.0f, 0.0f), 0.0f));
        objectsToPlace.Enqueue(new ObjectInstance(runningPlanctonPrefab, new Vector3(5.0f, 100.0f, 15.0f), 50.0f));

    }
    void PlaceRunningPlancton(float position, int count)
    {

    }
	void Init()
	{
		StartMusic ();
		StartPlayerMovement ();
        FirstLight.OnDisturb -= Init;
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
