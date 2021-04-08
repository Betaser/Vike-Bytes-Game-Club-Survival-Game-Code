using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        public int id;
        public string username;

        public Vector2 position;
        public float rotation;
        public int health;
        public bool attack;

        private float knockbackVelocity;
        private Vector2 knockbackDirection;

        public Dictionary<string, int> inventory = new Dictionary<string, int>();

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        private bool[] inputs;

        int attackTimer;
        int attackCooldown;
        bool input4LastFrame;

        public Player (int _id, string _username, Vector2 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            rotation = 0;
            health = 100;
            attack = false;
            attackTimer = 0;
            inventory["wood"] = 0;
            inventory["rock"] = 0;
            inventory["meat"] = 0;

            inputs = new bool[5];
        } 

        public void Update()
        {
            Vector2 _inputDirection = Vector2.Zero;
            if (inputs[0])
            {
                _inputDirection.Y += 1;
            }
            if (inputs[1])
            {
                _inputDirection.Y -= 1;
            }
            if (inputs[2])
            {
                _inputDirection.X -= 1;
            }
            if (inputs[3])
            {
                _inputDirection.X += 1;
            }
            if (inputs[4])
            {
                if(attackCooldown < 1 && !input4LastFrame)
                {
                    Attack();
                }
                input4LastFrame = true;
            }
            else
            {
                input4LastFrame = false;
            }

            if(attackCooldown > 0)
            {
                attackCooldown--;
            }

            if(attackTimer > 0)
            {
                attackTimer--;
            }
            else
            {
                attack = false;
            }



            Move(_inputDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            if(!(_inputDirection.X == 0 && _inputDirection.Y == 0))
            {
                position += Vector2.Normalize(_inputDirection) * moveSpeed;
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

            ServerSend.PlayerPosition(this);
        }

        public void SetInput(bool[] _inputs, float _rotation)
        {
            inputs = _inputs;
            rotation = _rotation;
        }

        public void ChangeHealth(int _healthDelta)
        {
            health += _healthDelta;
        }

        public void Attack()
        {
            attack = true;
            attackTimer = 3;
            attackCooldown = 6;
        }

        public void AddItem(string _type, int _count)
        {
            if (_type != "sword")
            {
                inventory[_type] += _count;
            }

            ServerSend.UpdateInventory(this);
        }
        public void Damage(int _healthDelta, int _animalId)
        {

            health -= _healthDelta;
            knockbackDirection = Vector2.Normalize(position - GameLogic.animals[_animalId].position);
            knockbackVelocity = 1;
        }
    }
}
