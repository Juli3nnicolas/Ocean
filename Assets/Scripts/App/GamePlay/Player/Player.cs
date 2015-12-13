using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Hand rightHand;
    private Hand leftHand;
	
	// Use this for initialization
	void Start () {
		rightHand = this.GetComponentsInChildren<Hand> ()[0];
        leftHand = this.GetComponentsInChildren<Hand>()[1];

        rightHand.Init(Vector3.zero);

        leftHand.Init(Vector3.zero);
    }

	// Update is called once per frame
	void Update () {
		
	}
    
}
