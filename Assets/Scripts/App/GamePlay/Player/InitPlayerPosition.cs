using UnityEngine;
using System.Collections;

namespace App
{
	namespace Gameplay
	{
		public class InitPlayerPosition : MonoBehaviour {
		
			public GameObject p_Camera;
		
			// Use this for initialization
			void Start () 
			{
				Renderer class_renderer = GetComponent<Renderer>();
				
				// Init player's position
				Vector3 player_init_offset = new Vector3(0.0f, class_renderer.bounds.size.y/2.0f, 0.0f);
				transform.position = player_init_offset;
				
				// Init camera position
				float const_camera_y_offset = 0.01f;
				Vector3 camera_init_offset = new Vector3(0.0f, class_renderer.bounds.size.y/2.0f + const_camera_y_offset, -1*class_renderer.bounds.size.z/2.0f);
				p_Camera.transform.position = transform.position + camera_init_offset;
				
				// Init Camera rotation
				p_Camera.transform.Rotate( new Vector3(-90.0f, 0.0f, 0.0f) );
			}
			
			// Update is called once per frame
			void Update () 
			{
			
			}
		}
	}
}
