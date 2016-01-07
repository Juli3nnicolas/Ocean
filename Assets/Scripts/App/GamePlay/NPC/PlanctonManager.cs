using UnityEngine;
using System.Collections;

public class PlanctonManager : MonoBehaviour, Core.StdInterfaces.Initiable
{
	/*
	 * Manage a plancton group
	 */
    public const int influenceZone = 10;
    private Hand rightHand;
    private Hand leftHand;

    //How much the player has interacted with plancton
    private float disturbance;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
		//Listen to the player's moves
        rightHand = GameObject.Find("RightHand").GetComponent<Hand>();
        rightHand.OnHandMove += Move;
        leftHand = GameObject.Find("LeftHand").GetComponent<Hand>();
        leftHand.OnHandMove += Move;
        disturbance = 0.0f;
    }
    public void Terminate()
    {
        rightHand.OnHandMove -= Move;
        leftHand.OnHandMove -= Move;
        Destroy(this.gameObject);
    }
    void Move(object hand, System.EventArgs args)
    {
		//In reaction to the player movements, we move
        Vector3 movement = ComputeMoves(rightHand) + ComputeMoves(leftHand);
        transform.Translate(movement);
        float individualMoveSize = ComputeIndividualMoveSize(rightHand) + ComputeIndividualMoveSize(leftHand);
		//And the we tell individuals to move
        if (individualMoveSize > 0.0f)
        {
            foreach (IndividualPlancton p in this.GetComponentsInChildren<IndividualPlancton>())
            {
                p.Move(individualMoveSize);
            }
            ChangeColor(new Color((1 - this.disturbance / 5.0f), disturbance / 5.0f, Mathf.Log(disturbance)));
        }

    }
	//The color of the bank change with the disturbance
    void ChangeColor(Color newColor)
    {
        foreach (IndividualPlancton p in this.GetComponentsInChildren<IndividualPlancton>())
        {
            p.ChangeColor(newColor);
        }
    }
    private Vector3 ComputeMoves(Hand hand)
    {

        float distance = Vector3.Distance(this.transform.position, hand.transform.position);
        Vector3 movement = Vector3.zero;
        if (distance < influenceZone)
        {
            Vector3 handMovement = hand.GetMove();
            if (disturbance < 10 - (Mathf.Abs(handMovement.x + handMovement.y + handMovement.z)))
                disturbance += Mathf.Abs(handMovement.x + handMovement.y + handMovement.z);
            movement.x = handMovement.y * Mathf.Pow((influenceZone - distance), 2) * (Mathf.Exp(-Mathf.Pow((disturbance - 4) / 3, 2))) * Random.Range(0.2f, 8.0f);

            movement.z = handMovement.z * Mathf.Pow((influenceZone - distance), 2) * (Mathf.Exp(-Mathf.Pow((disturbance - 4) / 3, 2))) * Random.Range(0.2f, 8.0f);
        }
        return movement;

    }
    float ComputeIndividualMoveSize(Hand hand)
    {
        float distance = Vector3.Distance(this.transform.position, hand.transform.position);
        if (distance < influenceZone)
            return Mathf.Abs(hand.GetMove().x) + Mathf.Abs(hand.GetMove().y) + Mathf.Abs(hand.GetMove().z);
        return 0.0f;
    }

}
