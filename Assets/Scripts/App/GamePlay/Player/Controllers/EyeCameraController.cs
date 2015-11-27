using UnityEngine;
using System.Collections;

namespace App
{
	namespace Gameplay
	{
		namespace Controllers
		{ 
			public class EyeCameraController : MonoBehaviour 
			{
				public GameObject p_Camera;
			
				// Use this for initialization
				void Start () 
				{
					m_initialOrientation = GetComponent<App.Gameplay.InitPlayerPosition>().getInitialOrientation();
				}
				
				// Update is called once per frame
				void Update () 
				{
					float dt = Time.deltaTime;
					rotateCamera(dt);
				}
				
				// Do not change the rotation order
				private void rotateCamera(float deltaTime)
				{
					float mouse_h = Input.GetAxis("Mouse X");
					float mouse_v = Input.GetAxis("Mouse Y");
					
					// Request to rotate the camera?
					if ( mouse_h != 0 || mouse_v != 0)
					{
						Vector2 new_angle = new Vector2(m_angle.x + mouse_v*m_angularSpeed.x*deltaTime, m_angle.y + mouse_h*m_angularSpeed.y*deltaTime);
					
						// Manage side rotations
					
						// Position reorientation to prevent unexpected rotations
						p_Camera.transform.rotation = m_initialOrientation;
						
						// Rotate to the left
						if ( new_angle.y > -90.0f && mouse_h < 0 ) // The rotation angle won't exceed the threshold value
							m_angle.y = new_angle.y;
						else
						{
							// The complete rotation can't be performed because the angle is greater than the max accepted value
							// However, if this test is true then part of the rotation can be done to reach the max value.
							if ( m_angle.y > -90.0f && mouse_h < 0 )
								m_angle.y = -90.0f;
						}
						
						// Rotate to the right
						if ( new_angle.y < 90.0f && mouse_h > 0 ) // The rotation angle won't exceed the threshold value
							m_angle.y = new_angle.y;
						else
						{
							// The complete rotation can't be performed because the angle is greater than the max accepted value
							// However, if this test is true then part of the rotation can be done to reach the max value.
							if ( m_angle.y < 90.0f && mouse_h > 0 )
								m_angle.y = 90.0f;
						}
						
						// Apply the rotation
						p_Camera.transform.RotateAround(p_Camera.transform.position, m_forwardAxis, m_angle.y);
						
						
						
						// Manage forward-backward rotations
							
						// Rotate forward
						if ( new_angle.x < 25.0f && mouse_v > 0 )
							m_angle.x = new_angle.x;
						else
						{
							if ( m_angle.x < 25.0f && mouse_v > 0 )
								m_angle.x = 25.0f;
						}
						
						// Rotate backward
						if ( new_angle.x > -20.0f && mouse_v < 0 )
							m_angle.x = new_angle.x;
						else
						{
							if ( m_angle.x > -20.0f && mouse_v < 0 )
								m_angle.x = -20.0f;
						}
						
						// Orient the camera whether it moved or not
						p_Camera.transform.RotateAround(p_Camera.transform.position, m_sideAxis, m_angle.x);
					}
				}
				
				// Rotate from side to side
				private void makeSideRotations(float deltaTime)
				{
					float mouse_h = Input.GetAxis("Mouse X");
					float mouse_v = Input.GetAxis("Mouse Y");
					
					
					// Position reorientation to prevent unexpected rotations
					p_Camera.transform.rotation = m_initialOrientation;
					
					// Rotate to the left
					if ( m_angle.y > -90.0f && mouse_h < 0 )
						m_angle.y = m_angle.y + mouse_h*m_angularSpeed.y*deltaTime;
					
					// Rotate to the right
					if ( m_angle.y < 90.0f && mouse_h > 0 )
						m_angle.y = m_angle.y + mouse_h*m_angularSpeed.y*deltaTime;
					
					// Apply the rotation
					p_Camera.transform.RotateAround(p_Camera.transform.position, m_forwardAxis, m_angle.y);
					
				}
				
				// Rotate backward and forward
				// Watch out! This method relies on makeSideRotations
				// and expects it to be called before
				private void makeForwardRotations(float deltaTime)
				{
					float mouse_v = Input.GetAxis("Mouse Y");
						
					// Rotate forward
					if ( m_angle.x < 45.0f && mouse_v > 0 )
						m_angle.x = m_angle.x + mouse_v*m_angularSpeed.x*deltaTime;
					
					// Rotate backward
					if ( m_angle.x > -20.0f && mouse_v < 0 )
						m_angle.x = m_angle.x + mouse_v*m_angularSpeed.x*deltaTime;
					
					// Orient the camera whether it moved or not
					p_Camera.transform.RotateAround(p_Camera.transform.position, m_sideAxis, m_angle.x);
				}
				
				////////////// Attributes //////////////
				
				private Quaternion m_initialOrientation;
				private Vector2    m_angularSpeed = new Vector2(30.0f, 30.0f); // In degree per seconds
				private Vector2    m_angle        = new Vector2(0.0f, 0.0f);   // 1st coordinate: side; 2nd: forward
				private Vector3    m_sideAxis     = new Vector3(1.0f, 0.0f, 0.0f);
				private Vector3    m_forwardAxis  = new Vector3(0.0f, 0.0f, 1.0f);
			}
		}
	}
}
