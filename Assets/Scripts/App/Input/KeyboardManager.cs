using UnityEngine;
using System.Collections;

public class KeyboardManager : MonoBehaviour {

	private float coef; 
	// Use this for initialization
	void Start () {
		coef = 0.01f;
	}
	
	// Update is called once per frame
	void Update () {

		KeyboardMovements ();
	
	}

	void KeyboardMovements()  
	{  
		Vector3 right = Vector3.zero;
		Vector3 left = Vector3.zero;
		if(Input.GetKey(KeyCode.Q))  
		{  
			right+=(Vector3.right*coef);
		}  
		if(Input.GetKey(KeyCode.D))  
		{  
			right+=(Vector3.left*coef);
		} 
		if(Input.GetKey(KeyCode.Z))  
		{  
			right+=(Vector3.up*coef); 
		}  
		if(Input.GetKey(KeyCode.S))  
		{  
			right+=(Vector3.down*coef);
		}  
		if(Input.GetKey(KeyCode.A))  
		{  
			right+=(Vector3.forward*coef); 
		}  
		if(Input.GetKey(KeyCode.E))  
		{  
			right+=(Vector3.back*coef);
        }
        if (Input.GetKey(KeyCode.K))
        {
            left += (Vector3.right * coef);
        }
        if (Input.GetKey(KeyCode.M))
        {
            left += (Vector3.left * coef);
        }
        if (Input.GetKey(KeyCode.O))
        {
            left += (Vector3.up * coef);
        }
        if (Input.GetKey(KeyCode.L))
        {
            left += (Vector3.down * coef);
        }
        if (Input.GetKey(KeyCode.I))
        {
            left += (Vector3.forward * coef);
        }
        if (Input.GetKey(KeyCode.P))
        {
            left += (Vector3.back * coef);
        }
        if (right != Vector3.zero || left !=Vector3.zero)
		    InputManager.Instance.MakeMoveHand (right, left);
	}  
}
