public interface IGameServerState
{
    void ProcessMessage(GameServer gameServer, NetworkMessage netMsg, INetworkAddress address);
    void Update(GameServer gameServer);
}
