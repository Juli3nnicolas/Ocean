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
				
				}
				
				// Update is called once per frame
				void Update () 
				{
					float dt = Time.deltaTime;
					makeForwardRotations(dt);
					makeSideRotations(dt);
				}
				
				// Rotate from side to side
				private void makeSideRotations(float deltaTime)
				{
					float mouse_h = Input.GetAxis("Mouse X");
					
					// Rotate to the left
					if ( m_angle.y > -90.0f && mouse_h < 0 )
					{
						float new_angle = m_angle.y + mouse_h*m_angularSpeed.y*deltaTime;
						p_Camera.transform.RotateAround(p_Camera.transform.position, m_forwardAxis, new_angle - m_angle.y);
						m_angle.y = new_angle;
					}
					
					// Rotate to the right
					if ( m_angle.y < 90.0f && mouse_h > 0 )
					{
						float new_angle = m_angle.y + mouse_h*m_angularSpeed.y*deltaTime;
						p_Camera.transform.RotateAround(p_Camera.transform.position, m_forwardAxis, new_angle - m_angle.y);
						m_angle.y = new_angle;
					}
				}
				
				// Rotate backward and forward
				private void makeForwardRotations(float deltaTime)
				{
					float mouse_v = Input.GetAxis("Mouse Y");
					
					// Rotate forward
					if ( m_angle.x < 45.0f && mouse_v > 0 )
					{
						float new_angle = m_angle.x + mouse_v*m_angularSpeed.x*deltaTime;
						p_Camera.transform.RotateAround(p_Camera.transform.position, m_sideAxis, new_angle - m_angle.x);
						m_angle.x = new_angle;
					}
					
					// Rotate backward
					if ( m_angle.x > -20.0f && mouse_v < 0 )
					{
						float new_angle = m_angle.x + mouse_v*m_angularSpeed.x*deltaTime;
						p_Camera.transform.RotateAround(p_Camera.transform.position, m_sideAxis, new_angle - m_angle.x);
						m_angle.x = new_angle;
					}
						
				}
				
				////////////// Attributes //////////////
				
				private Vector2 m_angularSpeed = new Vector2(30.0f, 30.0f); // In degree per seconds
				private Vector2 m_angle        = new Vector2(0.0f, 0.0f);   // 1st coordinate: side; 2nd: forward
				private Vector3 m_sideAxis     = new Vector3(1.0f, 0.0f, 0.0f);
				private Vector3 m_forwardAxis  = new Vector3(0.0f, 0.0f, 1.0f);
			}
		}
	}
}
