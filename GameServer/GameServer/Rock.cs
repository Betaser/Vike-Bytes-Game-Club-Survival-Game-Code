using System;
using System.Numerics;

namespace GameServer
{
	public class Rock
	{
		private short x;
		private short y;
		private int id;
		private int hp;

		public Rock(short x, short y, int id)
		{
			this.x = x;
			this.y = y;
			this.id = id;
			this.hp = 100;
		}

		public short getX()
		{
			return x;
		}
		public short getY()
		{
			return y;
		}
		public int getId()
		{
			return id;
		}
		public void setHp(int delta)
		{
			hp += delta;
			ServerSend.UpdateHp(this);
		}
		public int getHp()
		{
			return hp;
		}
	}
}
