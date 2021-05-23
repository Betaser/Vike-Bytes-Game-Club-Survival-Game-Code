using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerMovement(bool[] _inputs, float _rotation)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);
            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }
            _packet.Write(_rotation);

            SendUDPData(_packet);
        }
    }

    public static void ChangeHealth(int _healthDelta)
    {
        using (Packet _packet = new Packet((int)ClientPackets.changeHealth))
        {
            _packet.Write(_healthDelta);

            SendTCPData(_packet);
        }
    }

    public static void CreateAnimal(string _species)
    {
        using (Packet _packet = new Packet((int)ClientPackets.createAnimal))
        {
            _packet.Write(_species);

            SendTCPData(_packet);
        }
    }

    public static void Hit(string _type, int _id, int _damage)
    {
        using (Packet _packet = new Packet((int)ClientPackets.hit))
        {
            _packet.Write(_type);
            _packet.Write(_id);
            _packet.Write(_damage);

            SendTCPData(_packet);
        }
    }

    public static void AddItem(string _item, int _count)
    {
        using (Packet _packet = new Packet((int)ClientPackets.addItem))
        {
            _packet.Write(_item);
            _packet.Write(_count);

            SendTCPData(_packet);
        }
    }

    public static void Ready()
    {
        using (Packet _packet = new Packet((int)ClientPackets.ready))
        {
            SendTCPData(_packet);
        }
    }

    public static void PlayerDamage(int _damage, int _id)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerDamage))
        {
            _packet.Write(_damage);
            _packet.Write(_id);
            SendTCPData(_packet);
        }
    }
    #endregion
}
