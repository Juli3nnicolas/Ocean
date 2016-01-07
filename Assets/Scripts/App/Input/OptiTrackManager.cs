using UnityEngine;
using System.Collections;

public class OptiTrackManager : MonoBehaviour {

	private GameObject leftHand;
	private GameObject rightHand;
    private Player player;
    
	// Use this for initialization
	void Start () {
        //Objects used to track
		leftHand = GameObject.Find("mainGauche");
		rightHand = GameObject.Find("mainDroite");


        //Parent of the objects to move
        player = GameObject.Find("Player").GetComponent<Player>();
    }
	
	// At each update we set the hands positions (relative to the player's position) at the same value as tracked objects
	void Update () {

        
        InputManager.Instance.ChangePositionHand(player.transform.InverseTransformPoint(player.transform.position + rightHand.transform.localPosition), player.transform.InverseTransformPoint(player.transform.position + leftHand.transform.localPosition));
	
	}
}
