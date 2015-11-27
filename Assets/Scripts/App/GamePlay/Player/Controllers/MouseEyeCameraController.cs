using UnityEngine;
using System.Collections;
using App.Gameplay.Controllers;

namespace App
{
	namespace Gameplay
	{
		namespace Controllers
		{ 
			public class MouseEyeCameraController : EyeCameraController
			{	
				protected override void init()
				{
					base.init();
				}
				
				protected override void update()
				{
					base.update();
				}
				
				protected override Vector2 getMotion()
				{
					return new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") );
				}
				
				// Use this for initialization
				void Start () { init(); }
				
				// Update is called once per frame
				void Update () { update(); } 
			}
		}
	}
}
