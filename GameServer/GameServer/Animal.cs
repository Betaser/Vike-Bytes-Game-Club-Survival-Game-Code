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
        private float roamSpeed = 1.5f / Constants.TICKS_PER_SEC;
        private float viewDistance = 20;

        private Vector2 knockbackDirection;
        private float knockbackVelocity;

        private bool occupied = false;
        private const float roamDistanceMin = 4;
        private const float roamDistanceMax = 7;
        private Vector2 targetPosition;
        private int waitTimer;
        private const int waitTimeMin = 30;
        private const int waitTimeMax = 120;

        public Animal (int _id, string _species, Vector2 _spawnPosition)
        {
            id = _id;
            species = _species;
            position = _spawnPosition;
            targetPosition = _spawnPosition;
            rotation = 0f;
            health = 100;
        } 

        public void Update()
        {
            if (health <= 0)
            {
                GameLogic.respawnAnimal(id);
            }
            float searchDistance = viewDistance;
            foreach(Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    if (_client.player.spectating) {
                        // Console.WriteLine(false);
                        // occupied = false;
                        checkWaitTimer();
                        continue;
                    }
                    // Console.WriteLine(true);
                    float playerDistance = Vector2.DistanceSquared(_client.player.position, position);
                    if (playerDistance < searchDistance)
                    {
                        searchDistance = playerDistance;
                        if(species == "wolf")
                        {
                            targetPosition = _client.player.position;
                        }
                        else if(species == "hare")
                        {
                            targetPosition = position - (_client.player.position - position);
                        }
                        occupied = false;
                        waitTimer = 0;
                        break;
                    }
                    else
                    {
                        checkWaitTimer();
                    }
                }
            }
            if (waitTimer > 0)
            {
                waitTimer--;
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

            if (knockbackVelocity > 0)
            {
                position += knockbackVelocity * knockbackDirection;
                knockbackVelocity -= 0.3f;
                if (knockbackVelocity < 0)
                {
                    knockbackVelocity = 0;
                }
            }

            Move(_moveDirection);
        }

        private void checkWaitTimer ()
        {
            if (waitTimer <= 0)
            {
                if (!occupied)
                {
                    Random numGenerator = new Random();
                    double randomAngle = numGenerator.NextDouble() * 2 * Math.PI;
                    targetPosition = position + new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle)) * (float)(numGenerator.NextDouble() * (roamDistanceMax - roamDistanceMin) + roamDistanceMin);
                    occupied = true;
                }
                else
                {
                    if (Vector2.DistanceSquared(position, targetPosition) < 0.5)
                    {
                        Random numGenerator = new Random();
                        if (waitTimer <= 0) waitTimer = numGenerator.Next(waitTimeMin, waitTimeMax);
                        occupied = false;
                    }
                }
            }
        }

        private void Move(Vector2 _inputDirection)
        {
            if (!(_inputDirection.X == 0 && _inputDirection.Y == 0))
            {
                position += Vector2.Normalize(_inputDirection) * (occupied ? roamSpeed : moveSpeed) * (waitTimer > 0 ? 0 : 1);
            }

            ServerSend.AnimalData(this);
        }

        public void ChangeHealth(int _healthDelta)
        {
            health += _healthDelta;
        }

        public void Hit(int _damage, int _playerID)
        {
            health -= _damage;
            knockbackDirection = Vector2.Normalize(position - Server.clients[_playerID].player.position);
            knockbackVelocity = 1;
            ServerSend.UpdateHp(this);
        }
    }
}
