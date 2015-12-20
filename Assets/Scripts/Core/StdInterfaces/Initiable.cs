using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.StdInterfaces
{
    interface Initiable
    {
        // Start off object execution
        void Init();
        // Destroy Object
        void Terminate();
    }
}
