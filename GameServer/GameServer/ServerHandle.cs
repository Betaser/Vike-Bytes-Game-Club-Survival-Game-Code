using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }

            Server.clients[_fromClient].SendIntoGame(_username);
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            bool[] _inputs = new bool[_packet.ReadInt()];
            for(int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }
            float _rotation = _packet.ReadFloat();

            Server.clients[_fromClient].player.SetInput(_inputs, _rotation);
        }

        public static void ChangeHealth(int _fromClient, Packet _packet)
        {
            int _healthDelta = _packet.ReadInt();
            Server.clients[_fromClient].player.ChangeHealth(_healthDelta);
        }

        public static void Ready(int _fromClient, Packet _packet)
        {
            if (!Server.clients[_fromClient].player.ready)
            {
                Server.clients[_fromClient].player.ready = true;
                GameLogic.readyPlayers++;
            }
            
        }

        public static void CreateAnimal(int _fromClient, Packet _packet)
        {
            string _species = _packet.ReadString();
            Console.Error.WriteLine("Create animal no longer exists");
        }

        public static void Hit(int _fromClient, Packet _packet)
        {
            string _type = _packet.ReadString();
            int _id = _packet.ReadInt();
            int _damage = _packet.ReadInt();
            if(_type == "tree")
            {
                GameLogic.trees[_id].setHp(-_damage);
            } else if (_type == "rock")
            {
                GameLogic.rocks[_id].setHp(-_damage);
            } else if (_type == "animal")
            {
                GameLogic.animals[_id].Hit(_damage, _fromClient);
            }
        }

        public static void AddItem(int _fromClient, Packet _packet)
        {
            string _type = _packet.ReadString();
            int _count = _packet.ReadInt();
            Server.clients[_fromClient].player.AddItem(_type, _count);
        }

        public static void PlayerDamage(int _fromClient, Packet _packet)
        {
            int damage = _packet.ReadInt();
            int animalId = _packet.ReadInt();
            Server.clients[_fromClient].player.Damage(damage, animalId);
        }

        public static void Buy(int _fromClient, Packet _packet)
        {
            string wantedItem = _packet.ReadString();

            InventoryItem item = InventoryItem.getInventoryItemByName(wantedItem);

            if (item == null) return;

            Dictionary<string, int>.KeyCollection ingredients = item.ingredients.Keys;
            // make sure all ingredients are there
            foreach (string ingredient in ingredients)
            {
                if (Server.clients[_fromClient].player.inventory[ingredient] < item.ingredients[ingredient])
                {
                    return;
                }
            }
            // take away ingredients
            foreach (string ingredient in ingredients)
            {
                Server.clients[_fromClient].player.inventory[ingredient] -= item.ingredients[ingredient];
            }

            ServerSend.UpdateInventory(Server.clients[_fromClient].player);

        }
    }
}
