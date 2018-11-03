using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class TextConnection : Ssg.Core.Networking.Connection
{
    private StreamWriter m_writeFile;
    private StreamReader m_readFile;

    public TextConnection(string writeFileName, string readFileName)
    {
        var writeFileStream = new FileStream(writeFileName, FileMode.Truncate, FileAccess.Write, FileShare.Read);
        m_writeFile = new StreamWriter(writeFileStream);

        var readFileStream = new FileStream(readFileName, FileMode.Open, FileAccess.Read, FileShare.Write);
        m_readFile = new StreamReader(readFileStream);
    }

    public Ssg.Core.Networking.Message GetMessage()
    {
        var text = m_readFile.ReadLine();
        if (text == null)
            return null;

        var msg = new Ssg.Core.Networking.Message();
        msg.Data = Encoding.ASCII.GetBytes(text);
        return msg;
    }

    public void Send(Ssg.Core.Networking.Message message)
    {
        SaveToFile(message.Data);
    }

    public void Close()
    {
        m_writeFile.Close();
        m_readFile.Close();
    }

    private void SaveToFile(byte[] data)
    {
        var text = Encoding.ASCII.GetString(data);
        m_writeFile.WriteLine(text);
        m_writeFile.Flush();
    }
}
