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
					float mouse_h = Input.GetAxis("Mouse X");
					float mouse_v = Input.GetAxis("Mouse Y");
					float t = Time.deltaTime;
					
					Vector3 cur_angles = p_Camera.transform.rotation.eulerAngles;
					
					p_Camera.transform.Rotate(mouse_v*m_angularSpeed.y*t, mouse_h*m_angularSpeed.x*t, 0.0f);
					
					// WIP
					//if ( cur_angles.x >= 45.0f && cur_angles.x <= 185.0f ) 
					//	p_Camera.transform.Rotate(mouse_v*m_angularSpeed.y*t, 0.0f/*mouse_h*m_angularSpeed.x*t*/, 0.0f);
						
					//if ( cur_angles.y >= 0.0f && cur_angles.y <= 180.0f )
					//	p_Camera.transform.Rotate(/*mouse_v*m_angularSpeed.y*t*/ 0.0f, mouse_h*m_angularSpeed.x*t, 0.0f);
				}
				
				////////////// Attributes //////////////
				
				private Vector2 m_angularSpeed = new Vector2(30.0f, 30.0f); // In degree per seconds
			}
		}
	}
}
