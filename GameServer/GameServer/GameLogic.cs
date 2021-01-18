using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class GameLogic
    {
        public static Dictionary<int, Animal> animals = new Dictionary<int, Animal>();
        public static Tree[] trees = new Tree[Constants.TREE_COUNT];
        public static Rock[] rocks = new Rock[Constants.ROCK_COUNT];


        /// <summary>Runs all game logic.</summary>
        public static void Update()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.player.Update();
                }
            }

            foreach (Animal _animal in animals.Values)
            {
                if (_animal != null)
                {
                    _animal.Update();
                }
            }

            ThreadManager.UpdateMain();
        }

        public static void initializeGame ()
        {
            Random r = new Random();
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i] = new Tree((short)(r.Next(Constants.MAP_SIZE) - 21), (short)(r.Next(Constants.MAP_SIZE) - 21), i);
            }
            for (int i = 0; i < rocks.Length; i++)
            {
                rocks[i] = new Rock((short)(r.Next(Constants.MAP_SIZE) - 21), (short)(r.Next(Constants.MAP_SIZE) - 21), i);
            }
        }

        /* public static void MakeAllTrees ()
        {
            Random r = new Random();
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i] = new Tree((short)(r.Next(Constants.MAP_SIZE) - 21), (short)(r.Next(Constants.MAP_SIZE) - 21), i);
            }
        } */
        public static void CreateAnimal(string _species)
        {
            int _id = animals.Count;
            animals.Add(_id, new Animal(_id, _species, Vector2.Zero));
            ServerSend.SpawnAnimal(animals[_id]);
        }
    }
}
