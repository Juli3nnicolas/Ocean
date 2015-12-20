using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalibScreenCorners : MonoBehaviour {
    /*
    //private List<Vector3> _Positions = new List<Vector3>();

    public GameObject CalibPlane;
    public CalibMessageManager CalibMessageMgr;
    public Transform Stylus;

    //public Transform other;
    //public Transform other2;
    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update() {

        CalibPlane.GetComponentInChildren<MeshRenderer>().enabled = _Positions.Count < 3;

        if (Input.GetKeyDown(KeyCode.F2))
        {
            int progress = _Positions.Count;
            if (progress == 3)
            {
                _Positions.Clear();
                
            }
            else
            {
                _Positions.Add(Stylus.position);
            }

            CalibMessageMgr.setPogress(_Positions.Count);

            if (_Positions.Count == 3)
            {
                Compute();
            }
        }
    }

    void Compute()
    { 
        Vector3 tl = _Positions[0];
        Vector3 bl = _Positions[1];
        Vector3 br = _Positions[2];


        var leftToRight = (br - bl);
        var bottomToTop = (tl - bl);


        Vector3 center = (tl + br) / 2;

        Vector3 scaleFac = new Vector3(
            leftToRight.magnitude,
            bottomToTop.magnitude,
            1);


        CalibPlane.transform.position = center;
        CalibPlane.transform.localScale = scaleFac;


        
        var normal = Vector3.Cross(leftToRight, bottomToTop);
        //Debug.DrawRay(center, normal);
        //Debug.DrawRay(center, bottomToTop);

        //Quaternion.LookRotation()
        CalibPlane.transform.LookAt(center + normal, bottomToTop);

	}*/
}
