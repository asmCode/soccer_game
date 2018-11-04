using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class TextConnection : Ssg.Core.Networking.Connection
{
    private StreamWriter m_writeFile;
    private StreamReader m_readFile;

    public TextConnection(bool createFiles, string writeFileName, string readFileName)
    {
        var writeFileMode = createFiles ? FileMode.Create : FileMode.Truncate;
        var writeFileStream = new FileStream(writeFileName, writeFileMode, FileAccess.Write, FileShare.ReadWrite);
        m_writeFile = new StreamWriter(writeFileStream);

        var readFileMode = createFiles ? FileMode.Create : FileMode.Open;
        var readFileStream = new FileStream(readFileName, readFileMode, FileAccess.ReadWrite, FileShare.Write);
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
