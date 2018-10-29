using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class TextNetworkCommunication : NetworkCommunication
{
    private MessageSerializer m_msgSerializer;
    public StreamWriter m_outFile;
    public StreamReader m_inFile;

    public TextNetworkCommunication(string outFileName, string inFileName)
    {
        m_msgSerializer = new TextMessageSerializer();

        var outFileStream = new FileStream(inFileName, FileMode.Truncate, FileAccess.Write, FileShare.Read);
        m_outFile = new StreamWriter(outFileStream);

        var inFileStream = new FileStream(inFileName, FileMode.Open, FileAccess.Read, FileShare.Write);
        m_inFile = new StreamReader(inFileStream);
    }

    public override void SendMessage(Message message)
    {
        var data = m_msgSerializer.Serialize(message);
        if (data == null)
        {
            Debug.LogWarning("Unknown network message");
            return;
        }

        SaveToFile(data);
    }

    public override Message GetMessage()
    {
        var text = m_inFile.ReadLine();
        if (text == null)
            return null;

        return m_msgSerializer.Deserialize(Encoding.ASCII.GetBytes(text));
    }

    public override void Cleanup()
    {
        m_outFile.Close();
        m_inFile.Close();
    }

    private void SaveToFile(byte[] data)
    {
        var text = Encoding.ASCII.GetString(data);
        m_outFile.WriteLine(text);
        m_outFile.Flush();
    }
}
