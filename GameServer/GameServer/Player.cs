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
        public int sprite;
        public int health;
        public int attack;

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
            sprite = 3;
            health = 100;
            attack = -1;
            attackTimer = 0;

            inputs = new bool[5];
        } 

        public void Update()
        {
            Vector2 _inputDirection = Vector2.Zero;
            if (inputs[0])
            {
                _inputDirection.Y += 1;
                sprite = 1;
            }
            if (inputs[1])
            {
                _inputDirection.Y -= 1;
                sprite = 3;
            }
            if (inputs[2])
            {
                _inputDirection.X -= 1;
                sprite = 2;
            }
            if (inputs[3])
            {
                _inputDirection.X += 1;
                sprite = 0;
            }
            if (inputs[4])
            {
                if(attackCooldown < 1 && !input4LastFrame)
                {
                    Attack(sprite);
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
                attack = -1;
            }

            Move(_inputDirection);
        }

        private void Move(Vector2 _inputDirection)
        {
            if(!(_inputDirection.X == 0 && _inputDirection.Y == 0))
            {
                position += Vector2.Normalize(_inputDirection) * moveSpeed;
            }
            //position += _inputDirection * moveSpeed;

            ServerSend.PlayerPosition(this);
        }

        public void SetInput(bool[] _inputs)
        {
            inputs = _inputs;
        }

        public void ChangeHealth(int _healthDelta)
        {
            health += _healthDelta;
        }

        public void Attack(int _direction)
        {
            attack = _direction;
            attackTimer = 3;
            attackCooldown = 6;
        }
    }
}
