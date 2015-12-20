using UnityEngine;
using System.Collections;

public class VRCameraActivator : MonoBehaviour {
    public bool UsableInVR = false;
	// Use this for initialization
	void Start () {
        lo = gameObject.GetComponent<Camera>();
    }
    Camera lo;
	// Update is called once per frame
	void Update () {
        bool pres = UnityEngine.VR.VRDevice.isPresent;
        lo.enabled = UsableInVR;
          /*  (UsableInVR && pres) ||
            ((!UsableInVR) && (!pres)) ;*/
    }
}
