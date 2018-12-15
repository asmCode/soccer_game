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
        var sessionName = CreateSessionName();
        var dirName = CreateDirName(sessionName);
        m_dirInfo = Directory.CreateDirectory(dirName);

        var currentSessionFileName = Path.Combine(m_basePath, "current_session");
        System.IO.File.WriteAllText(currentSessionFileName, sessionName);

        Debug.Log("Starting text server. Session id: " + sessionName);
    }

    public Ssg.Core.Networking.Connection CheckNewConnections()
    {
        int newClientIndex = GetClientCount();
        if (CheckNewClient(newClientIndex))
        {
            var connection = CreateNewConnection(newClientIndex);
            return connection;
        }

        return null;
    }

    private string CreateSessionName()
    {
        return "session_" + System.DateTime.Now.Ticks.ToString();
    }

    private string CreateDirName(string sessionName)
    {
        return Path.Combine(m_basePath, sessionName);
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

    public override void StopServer()
    {
        throw new System.NotImplementedException();
    }

    public override byte[] RecvMessage()
    {
        throw new System.NotImplementedException();
    }
}
