using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet) {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector2 _position = _packet.ReadVector2();
        int _sprite = _packet.ReadInt();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _sprite);

    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        int _sprite = _packet.ReadInt();
        int _health = _packet.ReadInt();

        GameManager.players[_id].transform.position = _position;
        GameManager.players[_id].GetComponent<PlayerManager>().sprite = _sprite;
        GameManager.players[_id].GetComponent<PlayerManager>().health = _health;
    }
}
