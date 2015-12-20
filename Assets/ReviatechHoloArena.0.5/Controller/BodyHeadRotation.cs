using UnityEngine;
using System.Collections;

public class BodyHeadRotation : MonoBehaviour {
	public GameObject model;
	public GameObject modelHead;
	public GameObject trackerHead;
	public GameObject rightHandMarker;
	public GameObject leftHandMarker;

	GameObject mBodyMarker;

	// Use this for initialization
	void Start () {

	}

	
	// Update is called once per frame
	void LateUpdate () {
		HeadRotation ();
		BodyRotationWithoutMarker ();
	}

	void HeadRotation()
	{
		// Head Rotation
		modelHead.transform.rotation = trackerHead.transform.rotation;
	}

	void BodyRotationWithMarker()
	{
		// rotate using a marker
		model.transform.rotation = mBodyMarker.transform.rotation;
	}
	
	float mDistMin=0.1f;
	float mDistLMax=0.8f;

	void BodyRotationWithoutMarker()
	{
		// rotate using the head rotation or the hands position
		// Center beetween the hands
		Vector3 mHandsCenter = (rightHandMarker.transform.position + leftHandMarker.transform.position) / 2f;
		mHandsCenter = new Vector3 (mHandsCenter.x, model.transform.position.y, mHandsCenter.z);

		// Vector from body to center of hands
		Vector3 mBodyToCenter = mHandsCenter - model.transform.position;
		// Rotation beetween hands center and model forward
		Quaternion mBodyToCenterRotation = Quaternion.LookRotation (mBodyToCenter);

		// get the distance from the body to the center the the two hands
		float dotValue = Vector3.Dot(model.transform.forward,mBodyToCenter);
		float distance = Vector3.Distance (model.transform.position, model.transform.position + mBodyToCenter);
		if (distance < mDistMin)
			distance = mDistMin;
		
		if (distance > mDistLMax)
			distance = mDistLMax;
		if (dotValue < 0f)
			distance = mDistMin;




		// Head Angle
		float mHeadAngle = Mathf.Atan2(trackerHead.transform.forward.x, trackerHead.transform.forward.z) * Mathf.Rad2Deg;
		// Ratio
		float val = distance * (1f/(mDistLMax-mDistMin)) - mDistMin * (1f/(mDistLMax-mDistMin));
		// Hands Angle
		Quaternion mHandsAngle = Quaternion.AngleAxis(mBodyToCenterRotation.eulerAngles.y,model.transform.up);

		// Fuse Angle
		Quaternion angle = Quaternion.Lerp (mHandsAngle, Quaternion.AngleAxis(mHeadAngle,model.transform.up), 1f-val);

		model.transform.rotation=angle;

	}
}
