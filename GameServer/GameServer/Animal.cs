using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Animal
    {
        public int id;
        public string species;

        public Vector2 position;
        public float rotation;
        public int health;

        private float moveSpeed = 3f / Constants.TICKS_PER_SEC;
        private float viewDistance = 20;

        public Animal (int _id, string _species, Vector2 _spawnPosition)
        {
            id = _id;
            species = _species;
            position = _spawnPosition;
            rotation = 0f;
            health = 100;
        } 

        public void Update()
        {
            Vector2 targetPosition = position;
            float searchDistance = viewDistance;
            foreach(Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    float playerDistance = Vector2.DistanceSquared(_client.player.position, position);
                    if (playerDistance < searchDistance)
                    {
                        searchDistance = playerDistance;
                        targetPosition = _client.player.position;
                    }
                }
            }
            Vector2 _moveDirection = targetPosition - position;

            if (_moveDirection.X != 0)
            {
                rotation = MathF.Atan(_moveDirection.Y / _moveDirection.X) * 180f / ((float) Math.PI) + 90f;
                if (_moveDirection.X > 0)
                {
                    rotation += 180f;
                }
            }

            Move(_moveDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            if (!(_inputDirection.X == 0 && _inputDirection.Y == 0))
            {
                position += Vector2.Normalize(_inputDirection) * moveSpeed;
            }

            ServerSend.AnimalData(this);
        }

        public void ChangeHealth(int _healthDelta)
        {
            health += _healthDelta;
        }
    }
}
