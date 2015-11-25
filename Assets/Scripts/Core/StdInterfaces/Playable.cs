// Enable an object to be played or stopped.
// Useful when a behavior must be scheduled to happen in a complex way.

namespace Core
{
	namespace StdInterfaces
	{
		public interface Playable {
		
			// Start off object execution
			void init();
		
			// Resume object execution
			void play ();
			
			// Stop object execution
			void stop ();
			
			// End object execution
			void terminate();
		}
	}
}
