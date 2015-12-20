using UnityEngine;
using System.Collections;

public class Walking : MonoBehaviour {
	public Animator animator;
	public GameObject perso;
	public GameObject head;
    public GameObject bodyAndArm;
	public float persoSize=1.7f;
	Vector3 mLastPosition;
	float mSpeed=0f;

    public Transform GroundLevel;


	void Start()
	{
		mLastPosition = transform.position;
	}



	void Update() {
		// Place the char on the ground
		//perso.transform.position = new Vector3(head.transform.position.x,0f,head.transform.position.z);
        perso.transform.position = head.transform.position - (head.transform.position - GroundLevel.position).y * Vector3.up;
        //perso.transform.localPosition = new Vector3(perso.transform.localPosition.x, 0, perso.transform.localPosition.z);
        // animate the char using it's own speed
        var v = mSpeed;
		animator.speed = v;
		animator.gameObject.transform.position = perso.transform.position;
		//animator.gameObject.transform.rotation = perso.transform.rotation;

		// Height
		float mActuHeight = Vector3.Distance (head.transform.position, perso.transform.position);;
		float ratio = (mActuHeight / persoSize)*2f -1f;
		if (ratio < 0)
			ratio = 0;
		if (ratio > 1)
			ratio = 1;
		animator.SetFloat ("walk", ratio);
		float crouchRatio = 1f - ratio;

		if (crouchRatio < 0.2f)
			crouchRatio = 0.2f;
        
		animator.SetFloat ("crouch", crouchRatio);

        bodyAndArm.SetActive(crouchRatio < 0.35f);
        
	}

   
	// get the speed of the char
	void FixedUpdate()
	{
		mSpeed = Vector3.Distance(new Vector3(perso.transform.position.x,0f,perso.transform.position.z),mLastPosition) / Time.deltaTime;
		mLastPosition = new Vector3(perso.transform.position.x,0f,perso.transform.position.z);
	}
}
