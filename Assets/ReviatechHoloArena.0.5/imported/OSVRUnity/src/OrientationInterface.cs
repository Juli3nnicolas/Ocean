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
using System;

namespace OSVR
{
    namespace Unity
    {
        /// <summary>
        /// Orientation Interface: continually (or rather, when OSVR updates) updates its orientation based on the incoming tracker data.
        ///
        /// Attach to a GameObject that you'd like to have updated in this way.
        /// </summary>
        public class OrientationInterface : MonoBehaviour
        {
            public float compensationAng;
            public Vector3 compensationVec;
            /// <summary>
            /// The interface path you want to connect to.
            /// </summary>
            public string path;

            private OSVR.ClientKit.Interface iface;
            private OSVR.ClientKit.OrientationCallback cb;

            // Use this for initialization
            void Start()
            {
                if (0 == path.Length)
                {
                    Debug.LogError("Missing path for OrientationInterface " + gameObject.name);
                    return;
                }

                iface = OSVR.Unity.ClientKit.instance.context.getInterface(path);
                cb = new OSVR.ClientKit.OrientationCallback(callback);
                iface.registerCallback(cb, IntPtr.Zero);
            }
			
			
			#if THREAD_ACTIVATED
			private Quaternion data;
			private object LOCKER = new object();
			private System.Threading.AutoResetEvent NEW_DATA_EVENT = new System.Threading.AutoResetEvent(false);
			#endif

            private void callback(IntPtr userdata, ref OSVR.ClientKit.TimeValue timestamp, ref OSVR.ClientKit.OrientationReport report)
            {
#if THREAD_ACTIVATED
				var temp = Math.ConvertOrientation(report.rotation);
				lock (LOCKER)
				{
					data = temp;
				}
				NEW_DATA_EVENT.Set ();
#else
                transform.localRotation = Math.ConvertOrientation(report.rotation);
#endif
            }
			
			
			#if THREAD_ACTIVATED
			void Update()
			{
				if (NEW_DATA_EVENT.WaitOne(0))
				{
					lock (LOCKER)
					{
						transform.localRotation = /*Quaternion.AngleAxis(-compensationAng, compensationVec) **/ data * Quaternion.AngleAxis(compensationAng, compensationVec);
					}
				}
			}
			#endif

            void OnDestroy()
            {
                iface = null;
            }
        }
    }
}
