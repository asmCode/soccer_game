//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//public class FileCommunication : INetworkCommunication
//{
//    public FileCommunication(string sendFileName, string recvFileName)
//    {
//        var outFileStream = new FileStream(inFileName, FileMode.Truncate, FileAccess.Write, FileShare.Read);
//        m_outFile = new StreamWriter(outFileStream);

//        var inFileStream = new FileStream(inFileName, FileMode.Open, FileAccess.Read, FileShare.Write);
//        m_inFile = new StreamReader(inFileStream);
//    }

//    public void Send(byte[] data, int size, INetworkAddress address)
//    {
//        throw new System.NotImplementedException();
//    }

//    public bool Receive(byte[] data, out int size, out INetworkAddress address)
//    {
//        throw new System.NotImplementedException();
//    }

//    public void Close()
//    {
//        throw new System.NotImplementedException();
//    }
//}
