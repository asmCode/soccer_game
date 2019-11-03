using UnityEngine;

public class FakeAddress : INetworkAddress
{
    private string m_clientName;

    public FakeAddress(string clientName)
    {
        m_clientName = clientName;
    }

    public override string ToString()
    {
        return m_clientName;
    }

    public override bool Equals(object obj)
    {
        return
            obj is FakeAddress &&
            m_clientName.Equals(((FakeAddress)obj).m_clientName);
    }

    public override int GetHashCode()
    {
        return m_clientName.GetHashCode();
    }
}
