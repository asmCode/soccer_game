using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController
{
    // private NetworkCommunication m_netCom;

    public MessageQueue MessageQueue { get; private set; }

    public NetworkController()
    {
        //MessageQueue = new MessageQueue();
        //m_netCom = new TextNetworkCommunication("d:/dupa_out.txt", "d:/dupa_in.txt");
    }

    public void SendMessage(Message message)
    {
        // m_netCom.SendMessage(message);
    }

    public void Update()
    {
        //while (true)
        //{
        //    var msg = m_netCom.GetMessage();
        //    if (msg == null)
        //        break;

        //    MessageQueue.AddMessage(msg);
        //}
    }

    public void Cleanup()
    {
        //m_netCom.Cleanup();
    }
}
