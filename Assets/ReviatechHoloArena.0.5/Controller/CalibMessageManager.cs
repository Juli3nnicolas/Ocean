using UnityEngine;
using System.Collections;
using System;

public class CalibMessageManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void setPogress(int count)
    {
        GetComponent<Canvas>().enabled = count != 3;
        GetComponentInChildren<UnityEngine.UI.Text>().text = count.ToString(); ;
    }
}
