using System;
using System.Numerics;

using System.Collections.Generic;

namespace GameServer {
	public class InventoryItem
	{
		public static int allId;
		public int id;
		public string name;
		public Dictionary<string, int> ingredients;

		public static List<InventoryItem> items = new List<InventoryItem>();

		public InventoryItem(string name, Dictionary<string, int> ingredients)
        {
			this.name = name;
			this.ingredients = ingredients;
			this.id = allId;
			allId++;
        }

		// ONLY RUN ONCE!!!
		public void initializeItems ()
        {
			Dictionary<string, int> ingredients = new Dictionary<string, int>();
			// add all items
			ingredients["wood"] = 5;
			items.Add(new InventoryItem("Wood Armor", ingredients));
			ingredients.Clear();

			ingredients["iron"] = 1;
			ingredients["wood"] = 2;
			items.Add(new InventoryItem("Iron Spear", ingredients));
			ingredients.Clear();
		}

		//private Dictionary<string, int> getIngredientsToInitializeItem (str)
        //{

        //}
	}
}
