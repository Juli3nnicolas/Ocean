using UnityEngine;
using System.Collections;
using Core.StdInterfaces;

namespace App
{
	namespace Gameplay
	{

		public class MovePlayer : MonoBehaviour, Playable 
		{
			public float p_InitialSpeed; // Expressed in meter/second
			
			/////////// P L A Y A B L E   I N T E R F A C E ///////////
			
			// Start off object execution
			public void init()
			{
				m_verticalSpeed = p_InitialSpeed;
				play();
			}
			
			// Resume object execution
			public void play ()
			{
				// Resuming execution
				m_isPLaying = true;
				
				// Move the player along the world vertical axis
				float vert_velocity = m_verticalSpeed * Time.deltaTime;
				transform.Translate ( transform.forward*vert_velocity, Space.World );
			}
			
			// Stop object execution
			public void stop ()
			{
				if ( isPlaying () )
				{
					m_isPLaying = false;
				}
			}
			
			// End object execution
			public void terminate()
			{
				stop();
			}
			
			// Checking execution state
			public bool isPlaying()
			{
				return m_isPLaying;
			}
			
			/////////// M O N O   B E H A V I O R ///////////
			
			// Use this for initialization
			void Start ()
			{
			
			}
			
			// Update is called once per frame
			void Update () 
			{	
				if ( isPlaying() )
					play ();
			}
			
			/////////// C L A S S   A T T R I B U T E S ///////////
			
			private float m_verticalSpeed; // In meter/second
			private bool  m_isPLaying     = false;
		}
	}
}
