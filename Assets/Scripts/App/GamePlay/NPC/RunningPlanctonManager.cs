using UnityEngine;
using System.Collections;

public class RunningPlanctonManager : MonoBehaviour, Core.StdInterfaces.Initiable
{


    public enum states { Normal, Running, Grouping };

    public Vector3 destinationToPlayer; // Distance it tries to put between player and itself
    public Vector3 positionToPlayer; //Actual distance to player
    public Player player; //The player it is relative to
    public Hand rightHand; //The right Hand of the player
    public Hand leftHand; //The left Hand of the player
    public float speed; //The speed of the bank
    public float disturbance; //How much the player has disturbed plancton
    public float previousDisturbance;
    public float runningDisturbance; //The disturbance that init the movement
    public IndividualRunningPlancton[] childs;
    public float runTime;
    public states state;

    public const float disturbanceDistance = 10.0f;
    public const float timeToRun = 3.0f;

    void Start()
    {
        Init();
    }
    // Update is called once per frame
    void Update()
    {

        positionToPlayer = transform.position - player.transform.position;
        if (state == states.Normal)
        {
            MoveNormal();
        }
        if (state == states.Running)
        {
            SetPlanctonRun();
            if (Time.time > runTime + timeToRun)
            {
                SetGrouping();
            }
        }
        if (state == states.Grouping)
        {
            if (arePlanctonInOrder())
            {
                state = states.Normal;
            }
        }
        previousDisturbance = runningDisturbance;

    }

    public void Init()
    {
        //Member initialisation
        state = states.Normal;
        player = GameObject.Find("Player").GetComponent<Player>();
        destinationToPlayer = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(5.0f, 10.0f), Random.Range(-2.0f, 2.0f));
        speed = 0.01f;
        disturbance = 0.0f;
        previousDisturbance = disturbance;
        runningDisturbance = 0.0f;
        runTime = Time.time; ;

        //Setting listeners
        foreach (Hand hand in player.GetComponentsInChildren<Hand>())
        {
            hand.OnHandMove += Perturb;
        }
        childs = this.GetComponentsInChildren<IndividualRunningPlancton>();
        //Initialisating Individus
        foreach (IndividualRunningPlancton p in childs)
        {
            p.Init();
        }
    }
    public void Terminate()
    {

        foreach (Hand hand in player.GetComponentsInChildren<Hand>())
        {
            hand.OnHandMove -= Perturb;
        }
        Destroy(this.gameObject);
    }
    /**
    * Determine wether plancton should be perturbed by mouvement or not
    */
    void Perturb(object sender, System.EventArgs args)
    {
        Hand hand = (Hand)sender;
        float distancetoIndividuals=0.0f;
        foreach (IndividualRunningPlancton p in childs)
        {
            if (Vector3.Distance(p.transform.position, hand.transform.position) > distancetoIndividuals)
                distancetoIndividuals = Vector3.Distance(p.transform.position, hand.transform.position);
        }
        if (distancetoIndividuals < disturbanceDistance)
        {
            float move = sumVector3(hand.GetMove());
            float speed = sumVector3(hand.GetSpeed());
            disturbance += 0.2f * (move + speed) / (sumVector3(transform.position - hand.transform.position));
            if (disturbance > 1.5f)
            {
                if (state != states.Running)
                {
                    state = states.Running;
                    runningDisturbance = disturbance;
                }
                else
                {
                    runningDisturbance += disturbance;
                }
                disturbance = 0.0f;
                runTime = Time.time;

            }
        }

    }

    /** 
    * If the player don't perturb
    */
    void MoveNormal()
    {
        SetDestination();
        SetPositionToPlayer();
        transform.position = positionToPlayer + player.transform.position;

    }
    /**
     * If the player has perturbed planctons
     */
    void SetPlanctonRun()
    {
        //we tell the individual planctons to run away from player
        foreach (IndividualRunningPlancton p in childs)
        {
            p.SetDisturbance(player.transform.position);
        }

    }
    void SetGrouping()
    {
        //We set the position where to regroup
        Vector3 movement = player.transform.position + destinationToPlayer * 3 - transform.position;
        transform.position += movement;
        //Without affecting childs
        foreach (IndividualRunningPlancton p in childs)
        {
            p.transform.position = p.transform.position - movement;
        }

        //Disturbance ca be set to 0 again
        disturbance = 0.0f;
        previousDisturbance = 0.0f;
        runningDisturbance = 0.0f;

        //Ok, we're now in regrouping state
        state = states.Grouping;
        //we tell the individual planctons to regroup
        foreach (IndividualRunningPlancton p in childs)
        {
            p.Regroup();
        }

    }
    void SetDestination()
    {
        if (sumVector3(destinationToPlayer - positionToPlayer) < 0.2f)
        {
            destinationToPlayer.x = Mathf.Max(2.0f, destinationToPlayer.x - 0.2f);
            destinationToPlayer.y = Mathf.Max(5.0f, destinationToPlayer.y - 0.2f);
            destinationToPlayer.z = Mathf.Max(2.0f, destinationToPlayer.z - 0.2f);

        }
    }
    void SetPositionToPlayer()
    {
        positionToPlayer.x = ComputeCoordinates(positionToPlayer.x, destinationToPlayer.x);
        positionToPlayer.y = ComputeCoordinates(positionToPlayer.y, destinationToPlayer.y);
        positionToPlayer.z = ComputeCoordinates(positionToPlayer.z, destinationToPlayer.z);
    }
    float ComputeCoordinates(float origin, float target)
    {
        if (Mathf.Abs(target - origin) <= speed)
            return target;
        if ((target - origin) > 0)
        {
            return origin + Mathf.Max(speed, (target - origin) * speed);
        }
        return origin + Mathf.Min(-speed, (target - origin) * speed);
    }

    float sumVector3(Vector3 toSum)
    {
        return Mathf.Abs(toSum.x) + Mathf.Abs(toSum.y) + Mathf.Abs(toSum.z);
    }
    /**
     * Wether individuals plancton are ordered or still running
     */
    bool arePlanctonInOrder()
    {
        foreach (IndividualRunningPlancton p in childs)
        {
            if (!p.IsInPlace())
                return false;
        }
        return true;
    }


}
