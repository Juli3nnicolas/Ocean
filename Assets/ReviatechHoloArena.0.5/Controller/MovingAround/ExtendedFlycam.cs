using UnityEngine;
using System.Collections;

public class ExtendedFlycam : MonoBehaviour
{

    /*
	EXTENDED FLYCAM
		Desi Quintans (CowfaceGames.com), 17 August 2012.
		Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.
        Modified by Romain Lelong (reviatech.com), 2015+
 
	LICENSE
		Free as in speech, and free as in beer.
 
	FEATURES
		WASD/Arrows:    Movement
		          Q:    Climb
		          E:    Drop
                      Shift:    Move faster
                    Control:    Move slower
                        End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
	*/

    public float cameraSensitivity = 90;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public bool JoystickMode = true;
    public bool onlyHorizontal = false;

    public Transform transformReferenceToUse;

    void Start()
    {
        if (transformReferenceToUse == null)
            transformReferenceToUse = transform;
        if (!JoystickMode)
            Screen.lockCursor = true;
    }

    private bool axisV2Exists = true;


    void Update()
    {
        Vector3 forwardVector = transformReferenceToUse.forward;
        Vector3 sideVector = transformReferenceToUse.right;
        Vector3 upVector = transformReferenceToUse.up;

        if (onlyHorizontal)
        {
            forwardVector = new Vector3(forwardVector.x, 0, forwardVector.z);
            sideVector = new Vector3(sideVector.x, 0, sideVector.z);
            upVector =Vector3.zero;
        }

        if (!JoystickMode)
        {
            rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        }
        float elevationAxis=0;
        if (axisV2Exists)
        {
            try
            {
                elevationAxis = Input.GetAxis("Elevation");
                rotationX += Input.GetAxis("Horizontal2") * cameraSensitivity * Time.deltaTime;
                rotationY += Input.GetAxis("Vertical2") * cameraSensitivity * Time.deltaTime;
            }
            catch
            {
                axisV2Exists = false;
                Debug.LogWarning("Add axis named Elevation, Horizontal2 and Vertical2 to allow rotation and up/down");
            }
        }

        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            transform.position += forwardVector * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += sideVector * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            transform.position += forwardVector * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += sideVector * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
        else
        {
            transform.position += forwardVector * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += sideVector * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }

        
        if (Input.GetKey(KeyCode.Q)) { transform.position += upVector * climbSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.E)) { transform.position -= upVector * climbSpeed * Time.deltaTime; }

        transform.position += -elevationAxis * upVector * climbSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.End))
        {
            Screen.lockCursor = (Screen.lockCursor == false) ? true : false;
        }
    }
}