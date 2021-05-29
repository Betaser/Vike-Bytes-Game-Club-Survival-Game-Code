using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        /// <summary>Sends a packet to a client via TCP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        /// <summary>Sends a packet to a client via UDP.</summary>
        /// <param name="_toClient">The client to send the packet the packet to.</param>
        /// <param name="_packet">The packet to send to the client.</param>
        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="_exceptClient">The client to NOT send the data to.</param>
        /// <param name="_packet">The packet to send.</param>
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        /// <summary>Sends a welcome message to the given client.</summary>
        /// <param name="_toClient">The client to send the packet to.</param>
        /// <param name="_msg">The message to send.</param>
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);



                SendTCPData(_toClient, _packet);
;            }
        }

        public static void SendInitialize (int _toClient)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendInit))
            {
                _packet.Write(GameLogic.trees.Length);
                for (int i = 0; i < Constants.TREE_COUNT; i++)
                {
                    _packet.Write(GameLogic.trees[i].getId());
                    _packet.Write(GameLogic.trees[i].getX());
                    _packet.Write(GameLogic.trees[i].getY());
                }
                _packet.Write(GameLogic.rocks.Length);
                for (int i = 0; i < Constants.ROCK_COUNT; i++)
                {
                    _packet.Write(GameLogic.rocks[i].getId());
                    _packet.Write(GameLogic.rocks[i].getX());
                    _packet.Write(GameLogic.rocks[i].getY());
                }
                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnAnimal(Animal _animal)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnAnimal))
            {
                _packet.Write(_animal.id);
                _packet.Write(_animal.species);
                _packet.Write(_animal.position);
                _packet.Write(_animal.rotation);

                SendTCPDataToAll(_packet);
            }
        }

        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);
                _packet.Write(_player.health);
                _packet.Write(_player.attack);
                _packet.Write(_player.spectating);

                SendUDPDataToAll(_packet);
            }
        }

        public static void AnimalData(Animal _animal)
        {
            using (Packet _packet = new Packet((int)ServerPackets.animalData))
            {
                _packet.Write(_animal.id);
                _packet.Write(_animal.species);
                _packet.Write(_animal.position);
                _packet.Write(_animal.rotation);
                _packet.Write(_animal.health);

                SendUDPDataToAll(_packet);
            }
        }

        public static void UpdateHp(Tree _tree)
        {
            using (Packet _packet = new Packet((int)ServerPackets.updateHp))
            {
                _packet.Write("tree");
                _packet.Write(_tree.getId());
                _packet.Write(_tree.getHp());

                SendTCPDataToAll(_packet);
            }
        }

        public static void UpdateHp(Rock _rock)
        {
            using (Packet _packet = new Packet((int)ServerPackets.updateHp))
            {
                _packet.Write("rock");
                _packet.Write(_rock.getId());
                _packet.Write(_rock.getHp());

                SendTCPDataToAll(_packet);
            }
        }
        public static void UpdateHp(Animal _animal)
        {
            using (Packet _packet = new Packet((int)ServerPackets.updateHp))
            {
                _packet.Write("animal");
                _packet.Write(_animal.id);
                _packet.Write(_animal.health);

                SendTCPDataToAll(_packet);
            }
        }

        public static void UpdateInventory(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.updateInventory))
            {
                _packet.Write(_player.inventory["wood"]);
                _packet.Write(_player.inventory["rock"]);
                _packet.Write(_player.inventory["meat"]);

                SendTCPData(_player.id, _packet);
            }
        }
        #endregion
    }
}
