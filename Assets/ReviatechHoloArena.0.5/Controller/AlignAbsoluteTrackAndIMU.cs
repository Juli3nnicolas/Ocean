using System.Collections;
using UnityEngine;
using UnityEngine.VR;
using System;

public class AlignAbsoluteTrackAndIMU : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    public Transform SceneCenter;
    public Transform OnBoardDirection;

    
	
	public Transform compensatingDirection;
	public Transform absoluteDirection;
	

	
	public void MoveOffset(Vector3 off)
	{
		
		SceneCenter.position += off * .2f; 
	}

    internal void Rotate(int v, Vector3 vector3)
    {
        SceneCenter.Rotate(vector3, v);
    }

	
	// Update is called once per frame
	void Update () {


        if (OnBoardDirection != null)
        {
            if (true)//Input.GetMouseButtonDown(0))
            {
                

                //InputTracking.Recenter();

                if (UnityEngine.VR.VRDevice.isPresent)
                {
                    var curVal = compensatingDirection.localRotation.eulerAngles.y;
                    var trgVal = curVal + absoluteDirection.rotation.eulerAngles.y - OnBoardDirection.rotation.eulerAngles.y;
                    var tendency = (trgVal - curVal);

                    float bestCandidate = float.MaxValue;
                    for (int i = -2; i <= 2; i++)
                        if (Mathf.Abs(tendency + 360 * i) < Mathf.Abs(bestCandidate))
                            bestCandidate = tendency + 360 * i;

                    tendency = bestCandidate;
                    
                    compensatingDirection.localRotation = Quaternion.Euler(new Vector3(0, curVal + tendency * Time.deltaTime, 0));
                }
                else
                {
                    compensatingDirection.localRotation = Quaternion.identity;
                }
                
      
            }
        }
	}
}
