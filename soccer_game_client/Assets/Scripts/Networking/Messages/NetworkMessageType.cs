﻿public enum NetworkMessageType
{
    // Client
    JoinRequest,
    ReadyToStart,

    // Server
    JoinAccept,
    OpponentFound,
    StartMatch,

    // Match
    PlayerMove,
    PlayerPosition,
}
