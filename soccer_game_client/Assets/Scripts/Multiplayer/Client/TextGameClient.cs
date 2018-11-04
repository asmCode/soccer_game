using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TextGameClient : GameClient
{
    private string m_basePath;

    public TextGameClient(string basePath)
    {
        m_basePath = basePath;
    }

    public override Ssg.Core.Networking.Connection Connect()
    {
        var sessionName = GetCurrentSessionName();
        if (sessionName == null)
            return null;

        int clientIndex = GetAvailableClientIndex(sessionName);
        if (clientIndex == -1)
            return null;

        var sessionPath = GetSessionPath(sessionName);

        return CreateConnection(clientIndex, sessionPath);
    }

    private string GetCurrentSessionName()
    {
        var currentSessionFileName = System.IO.Path.Combine(m_basePath, "current_session");
        if (!System.IO.File.Exists(currentSessionFileName))
            return null;

        return System.IO.File.ReadAllText(currentSessionFileName);
    }

    private int GetAvailableClientIndex(string sessionName)
    {
        var sessionPath = GetSessionPath(sessionName);

        if (!CheckClientFilesExist(sessionPath, 0))
            return 0;

        if (!CheckClientFilesExist(sessionPath, 1))
            return 1;

        return -1;
    }

    private string GetSessionPath(string sessionName)
    {
        return System.IO.Path.Combine(m_basePath, sessionName);
    }

    private bool CheckClientFilesExist(string sessionPath, int index)
    {
        string clientInFile;
        string clientOutFile;
        CreateClientFileNames(index, sessionPath, out clientInFile, out clientOutFile);

        return System.IO.File.Exists(clientInFile) || System.IO.File.Exists(clientOutFile);
    }

    private void CreateClientFileNames(int clientIndex, string sessionPath, out string clientIn, out string clientOut)
    {
        clientOut = Path.Combine(sessionPath, "client_out_" + (clientIndex + 1).ToString());
        clientIn = Path.Combine(sessionPath, "client_in_" + (clientIndex + 1).ToString());
    }


    private Ssg.Core.Networking.Connection CreateConnection(int index, string sessionPath)
    {
        string clientInFile;
        string clientOutFile;
        CreateClientFileNames(index, sessionPath, out clientInFile, out clientOutFile);

        return new TextConnection(true, clientOutFile, clientInFile);
    }
}
