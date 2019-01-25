public interface INetworkFactory
{
    INetworkCommunication CreateCommunication();
    IServerAddressGetter CreateServerAddressGetter();
}
