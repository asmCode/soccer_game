using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ssg.Core.Networking
{
    public class NetworkMessage
    {
        public short Type { get; set; }
        public object Data { get; set; }
    }
}
