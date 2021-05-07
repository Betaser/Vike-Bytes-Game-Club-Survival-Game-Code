using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class GameLogic
    {
        public static Animal[] animals = new Animal[Constants.ANIMAL_COUNT];
        public static Tree[] trees = new Tree[Constants.TREE_COUNT];
        public static Rock[] rocks = new Rock[Constants.ROCK_COUNT];

        public static Dictionary<string, float> armor = new Dictionary<string, float>();


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

            foreach (Animal _animal in animals)
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
            for (int i = 0; i < Constants.ANIMAL_COUNT; i++)
            {
                animals[i] = new Animal(i, "hare", new Vector2((r.Next(Constants.MAP_SIZE) - 21), (r.Next(Constants.MAP_SIZE) - 21)));
                ServerSend.SpawnAnimal(animals[i]);
            }

            // add armor options
            armor.Add("nothing", 1f);
            armor.Add("wood", 0.85f);
            armor.Add("leather", 0.7f);
            armor.Add("armoor", 0.5f);
        }

        /* public static void MakeAllTrees ()
        {
            Random r = new Random();
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i] = new Tree((short)(r.Next(Constants.MAP_SIZE) - 21), (short)(r.Next(Constants.MAP_SIZE) - 21), i);
            }
        } */
        public static void respawnAnimal(int _id)
        {
            Random r = new Random();
            string species = animals[_id].species;
            if (species == "hare")
            {
                species = "wolf";
            }
            animals[_id] = new Animal(_id, species, new Vector2(r.Next(Constants.MAP_SIZE) - 21, r.Next(Constants.MAP_SIZE) - 21));
            ServerSend.SpawnAnimal(animals[_id]);
        }
    }
}
