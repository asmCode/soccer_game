using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextGameServer : GameServer
{
    private string m_basePath;
    private DirectoryInfo m_dirInfo;

    public TextGameServer(string basePath)
    {
        m_basePath = basePath;
    }

    public override void StartServer()
    {
        var dirName = CreateDirName();
        m_dirInfo = Directory.CreateDirectory(dirName);
    }

    public override void StopServer()
    {
    }

    public override Ssg.Core.Networking.Connection CheckNewConnections()
    {
        int newClientIndex = GetClientCount();
        if (CheckNewClient(newClientIndex))
        {
            var connection = CreateNewConnection(newClientIndex);
            return connection;
        }

        return null;
    }

    private string CreateDirName()
    {
        return Path.Combine(m_basePath, "session_" + System.DateTime.Now.Ticks.ToString());
    }

    private bool CheckNewClient(int index)
    {
        string clientIn;
        string clientOut;
        CreateClientFileNames(index, out clientIn, out clientOut);

        return File.Exists(clientIn) && File.Exists(clientOut);
    }

    private void CreateClientFileNames(int clientIndex, out string clientIn, out string clientOut)
    {
        clientOut =  Path.Combine(m_dirInfo.FullName, "client_out_" + (clientIndex + 1).ToString());
        clientIn = Path.Combine(m_dirInfo.FullName, "client_in_" + (clientIndex + 1).ToString());
    }

    private Ssg.Core.Networking.Connection CreateNewConnection(int index)
    {
        string clientIn;
        string clientOut;
        CreateClientFileNames(index, out clientIn, out clientOut);

        return new TextConnection(false, clientIn, clientOut);
    }
}
