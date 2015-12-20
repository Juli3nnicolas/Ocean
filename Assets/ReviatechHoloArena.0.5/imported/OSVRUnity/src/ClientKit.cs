/// OSVR-Unity Connection
///
/// http://sensics.com/osvr
///
/// <copyright>
/// Copyright 2014 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>
#define THREAD_ACTIVATED
using UnityEngine;

namespace OSVR
{
    namespace Unity
    {
        public class ClientKit : MonoBehaviour
		{
			public static System.Threading.ManualResetEvent activated = new System.Threading.ManualResetEvent(true);
			[Tooltip("A string uniquely identifying your application, in reverse domain-name format.")]
			public string AppID;
			[Tooltip("A string with the remote ip to connect to.")]
			public string hostName = "";

            private OSVR.ClientKit.ClientContext contextObject;

            /// <summary>
            /// Use to access the single instance of this object/script in your game.
            /// </summary>
            /// <returns>The instance, or null in case of error</returns>
            public static ClientKit instance
            {
                get
                {
                    ClientKit candidate = GameObject.FindObjectOfType<ClientKit>();
                    if (null == candidate)
                    {
                        Debug.LogError("OSVR Error: You need the ClientKit prefab in your game!!");
                    }
                    return candidate;

                }
            }

            /// <summary>
            /// Access the underlying Managed-OSVR client context object.
            /// </summary>
            public OSVR.ClientKit.ClientContext context
            {
                get
                {
                    EnsureStarted();
                    return contextObject;
                }
            }

            private void EnsureStarted()
            {
                if (!enabled)
                    return;

                if (contextObject == null)
                {
                    if (0 == AppID.Length)
                    {
                        Debug.LogError("OSVR ClientKit instance needs AppID set to a reverse-order DNS name! Using dummy name...");
                        AppID = "org.opengoggles.osvr-unity.dummy";
                    }
					Debug.Log("[OSVR] Starting with app ID: " + AppID + " and hostName: " + hostName);

					if (hostName != "")
					{
						Debug.Log("[OSVR] Starting with app ID: " + AppID + " and hostName2: " + hostName);
						contextObject = new OSVR.ClientKit.ClientContext(AppID, hostName, 0);
					}
					else
	                    contextObject = new OSVR.ClientKit.ClientContext(AppID, 0);
                }
            }

            void Awake()
            {
                DLLSearchPathFixer.fix();
                DontDestroyOnLoad(gameObject);
            }
            void Start()
            {
                Debug.Log("[OSVR] In Start()");
                EnsureStarted();
            }

            void OnEnable()
            {
                Debug.Log("[OSVR] In OnEnable()");
                EnsureStarted();
            }

			//rlel testvoid FixedUpdate()
			//rlel test{
                //EnsureStarted();				//rlel test
				//contextObject.update();//rlel test
			//rlel test}
          
#if THREAD_ACTIVATED
			System.Threading.Thread _RunningThread = null;
#endif
            //may seem superfluous. the goal here is to update the client more often to make sure we have the most recent tracker data
            //this helps reduce latency
            void Update()
			{
#if THREAD_ACTIVATED
				if (_RunningThread == null)
				{
					_RunningThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.decalUpdate));
					_RunningThread.Start();
				}
#else
                contextObject.update();
#endif
            }
            //may seem superfluous. the goal here is to update the client more often to make sure we have the most recent tracker data
            //this helps reduce latency
			//rlel testvoid LateUpdate()
			//rlel test{
			//rlel test	//contextObject.update();//rlel test
			//rlel test}
			
#if THREAD_ACTIVATED
			private System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch(); 
			void decalUpdate()
			{
				try
				{
				while(true)
				{
					sw.Reset();
					sw.Start();


						activated.WaitOne();


						contextObject.update();
						System.Threading.Thread.Sleep(1);

					int toWait = /*16*/ /*8*/ 4 - (int)(sw.ElapsedMilliseconds);	//TODO, éviter décalage de frame brutal... Ptet ne mettre aucune limite ?
					if (toWait > 0)
						System.Threading.Thread.Sleep(toWait);
				}
				}
				catch (System.Threading.ThreadAbortException tae)
				{
					//normal
					Debug.Log("Thread aborted thread.");

				}
				catch (System.Exception ee)
				{
					// not normal
					Debug.LogError(ee.ToString ());
				}

			}
#endif
            void Stop()
			{
				if (_RunningThread != null)
				{
					Debug.Log("Stopping thread.");
					_RunningThread.Abort();
					_RunningThread.Join();
					Debug.Log("Thread is dead.");
					_RunningThread = null;
				}

                if (null != contextObject)
                {
                    Debug.Log("Shutting down OSVR.");
                    contextObject.Dispose();
                    contextObject = null;
                }
            }

            void OnDisable()
            {
                Stop();
            }

            void OnDestroy()
            {
                Stop();
            }

            void OnApplicationQuit()
            {
                Stop();
            }
        }
    }
}
