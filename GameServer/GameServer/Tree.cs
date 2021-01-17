using System;
using System.Numerics;

public class Tree
{
	private short x;
	private short y;
	private int id;

	public Tree (short x, short y, int id)
	{
		this.x = x;
		this.y = y;
		this.id = id;
	}

	public short getX ()
    {
		return x;
    }
	public short getY ()
    {
		return y;
    }
	public int getId ()
    {
		return id;
    }
}
