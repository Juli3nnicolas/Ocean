using UnityEngine;
using System.Collections;
using OSVR.Unity;

public class AbsoluteRotateActivator : MonoBehaviour {

    OrientationInterface Follower;
	// Use this for initialization
	void Start () {
        Follower = GetComponent<OrientationInterface>();
    }
	
	// Update is called once per frame
	void Update () {
        bool vrOn = UnityEngine.VR.VRDevice.isPresent;
        Follower.enabled = !vrOn;
        if (vrOn) transform.localRotation = Quaternion.identity;
    }
}
