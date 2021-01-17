using System;

public class Item
{

	protected Item[] requirements;
	public String name;
	public Item(String name)
	{
		requirements = null;
		this.name = name;
	}
	public Item (Item[] requirements, String name)
    {
		this.requirements = requirements;
		this.name = name;
    }
}
