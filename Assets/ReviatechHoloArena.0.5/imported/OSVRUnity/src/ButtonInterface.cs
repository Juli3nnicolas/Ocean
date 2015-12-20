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

using System;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace OSVR
{
	namespace Unity
	{

public class ButtonInterface : InterfaceGameObject {

            // Use this for initialization

            public KeyCode AlternateKeyCode;

	new void Start()
	{
		osvrInterface.RegisterCallback(callback);
		//currentFrame = Time.frameCount;
		//frameCount = 0;
	}
	
	
	#if THREAD_ACTIVATED
	private List<int> data = new List<int> ();
	private object LOCKER = new object();
	private System.Threading.AutoResetEvent NEW_DATA_EVENT = new System.Threading.AutoResetEvent(false);
	#endif
	
	private bool WasPressed = false;
	
	private void callback(string source, bool pressed)
	{
				bool somethigHappened = false;
		int state=1;
		if (WasPressed != pressed)
		{
					somethigHappened = true;
			state = pressed ? 1 : -1;	// 1 if press, -1 if released
		}
				WasPressed = pressed;

		if (!somethigHappened)
			return;

		#if THREAD_ACTIVATED
		lock (LOCKER)
		{
			data.Add(state);
		}
		NEW_DATA_EVENT.Set ();
		#else
		data.Add(state);
		Process(data);
		#endif
	}
	
	#if THREAD_ACTIVATED
	void Update()
	{
                if (AlternateKeyCode != KeyCode.None)
                {
                    if (Input.GetKeyDown(AlternateKeyCode))
                    {
                        CallPress();
                    }

                    if (Input.GetKeyUp(AlternateKeyCode))
                    {
                        CallRelease();
                    }

                }
                if (NEW_DATA_EVENT.WaitOne(0))
		{
			List<int> ldata;
			lock (LOCKER)
			{
				ldata = new List<int>(data);
				data = new List<int>();
			}
			Process(ldata);
		}
	}
	#endif
	
	void Process(List<int> ldata)
	{
				foreach(var el in ldata)
		{
			if (el == 1)
				CallPress();
			else if (el == -1)
				CallRelease();
		}
				ldata.Clear ();
	}
	
	protected virtual void CallPress()
	{
		
	}
	
			protected virtual void CallRelease()
	{
		
	}
	
}
	}
}
