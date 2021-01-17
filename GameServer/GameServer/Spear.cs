using System;

public class Spear : Item
{
	public short damage;
	public Spear() : base(new Item[] { new Item("Wood"), new Item("Stone") }, "Spear")
	{
		this.damage = 50;
	}
}
