using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSerializerFactory
{
    public static MessageSerializer Create()
    {
        return new TextMessageSerializer();
    }
}
