using UnityEngine;
using System.Collections;

public class Scene1 : MonoBehaviour {


	private GameObject player;
    private GameObject firstLight;
    private bool started;

    //Count to manage objects of first scene
    private System.Collections.Generic.Queue<ObjectInstance> objectsToPlace;
    private System.Collections.Generic.Queue<ObjectDestroy> objectsToDestroy;

    //Prefabs
    public GameObject runningPlanctonPrefab;
    public GameObject planctonPrefab;
    public GameObject creaturePrefab;
    public GameObject loupiottePrefab;
    public GameObject finalLightPrefab;
	
	// Use this for initialization
	void Start () {

		player= GameObject.Find ("Player");
        firstLight = GameObject.Find("FirstLight");
        FirstLight.OnDisturb += Init;
		//We enqueu all object that will be instantiated
        InitObjectsToPlace();
        objectsToDestroy = new System.Collections.Generic.Queue<ObjectDestroy>();
        started = false;

	}
	
	// Update is called once per frame
	void Update () {


        Place();
        
		
	}
    void Place()
    {
		//If the player has reached the height required by an object to be instatiated,
		// The we instatiate it and we had it to the destroying queu
        while(objectsToPlace.Count > 0 &&  player.transform.position.y >= objectsToPlace.Peek().GetStep()) 
        {
            ObjectInstance instanceToCreate = objectsToPlace.Dequeue();
            GameObject newObject = (GameObject) Instantiate(instanceToCreate.GetObjectType(), instanceToCreate.GetPosition(), Quaternion.identity);
            objectsToDestroy.Enqueue(new ObjectDestroy(newObject, instanceToCreate.GetDestroyStep()));
            newObject.GetComponent<Core.StdInterfaces.Initiable>().Init();

		}//If the player has reached the height required by an object to be terminated,
		// The we terminate it
        while (objectsToDestroy.Count > 0 && player.transform.position.y >= objectsToDestroy.Peek().GetStep())
        {
            ObjectDestroy toDestroy =  objectsToDestroy.Dequeue();
            toDestroy.GetObject().GetComponent<Core.StdInterfaces.Initiable>().Terminate();
        }
		//The end of the scene, we load scene two
        if (player.transform.position.y >= 99.5f)
            Application.LoadLevel("Scene2");

    }
    void InitObjectsToPlace()
    {
        objectsToPlace = new System.Collections.Generic.Queue<ObjectInstance>();

        //We had instances of the objects we want to instantiate ordered by the height they must appear
		//See ObjectInstance class for more precisions
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(5.0f, 20.0f, 2.0f), 0.0f, 50.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(3.0f, 22.0f, 1.0f), 0.0f, 50.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(-4.0f, 23.0f, 3.2f), 0.0f, 50.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(-3.0f, 25.0f, 0.0f), 0.0f, 55.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 30.0f, 1.0f), 0.0f, 60.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 30.0f, 0.0f), 0.0f, 60.0f));

        for (float i = -3.0f; i <= 3.0f; i++)
        {
            for (float n = -3.0f; n <= 3.0f; n++)
            {
                objectsToPlace.Enqueue(new ObjectInstance(creaturePrefab, new Vector3(i*2, 40.0f, n*2), 0.0f, 60.0f));
            }
        }

        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 45.0f, 1.0f), 0.0f, 80.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(2.5f, 45.0f, 0.0f), 0.0f, 80.0f));
        objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.5f, 50.0f, 1.0f), 0.0f, 80.0f));
		objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 50.0f, 3.0f), 0.0f, 80.0f));
		objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 60.0f, 1.0f), 20.0f, 70.0f));
		objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 60.0f, 0.0f), 20.0f, 70.0f)); 
		for (float i = -3.0f; i <= 3.0f; i++)
		{
			for (float n = -3.0f; n <= 3.0f; n++)
			{
				objectsToPlace.Enqueue(new ObjectInstance(creaturePrefab, new Vector3(i*2, 80.0f, n*2), 20.0f, 100.0f));
			}
		}
		objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 80.0f, 0.0f), 40.0f, 100.0f)); 
		objectsToPlace.Enqueue(new ObjectInstance(planctonPrefab, new Vector3(0.0f, 90.0f, 0.0f), 40.0f, 100.0f)); 
        objectsToPlace.Enqueue(new ObjectInstance(runningPlanctonPrefab, new Vector3(5.0f, 100.0f, 15.0f), 55.0f, 150.0f));
		objectsToPlace.Enqueue(new ObjectInstance(loupiottePrefab, new Vector3(5.0f, 75.0f, 15.0f), 65.0f, 150.0f));
		objectsToPlace.Enqueue(new ObjectInstance(runningPlanctonPrefab, new Vector3(0.0f, 100.0f, 0.0f), 70.0f, 150.0f));
		objectsToPlace.Enqueue(new ObjectInstance(loupiottePrefab, new Vector3(5.0f, 85.0f, 15.0f), 75.0f, 150.0f));
        objectsToPlace.Enqueue(new ObjectInstance(finalLightPrefab, new Vector3(0.0f, 200.0f, 0.0f), 94.5f, 150.0f));

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

	//This function is called when the player disturb the first loupiotte
	//It initialize the game
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
