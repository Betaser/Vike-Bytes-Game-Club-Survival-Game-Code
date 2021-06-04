using System;
using System.Numerics;

using System.Collections.Generic;

namespace GameServer {
	public class InventoryItem
	{
		public static int allId = 3;
		public int id;
		public string name;
		public Dictionary<string, int> ingredients;

		public static Dictionary<string, int> nameToId = new Dictionary<string, int>();

		public static List<InventoryItem> items = new List<InventoryItem>();

		public InventoryItem(string name, Dictionary<string, int> ingredients)
        {
			this.name = name;
			this.ingredients = ingredients;
			this.id = allId;
			nameToId[name] = this.id;
			allId++;
        }

		// ONLY RUN ONCE!!!
		public void initializeItems ()
        {
			nameToId["wood"] = 0;
			nameToId["stone"] = 1;
			nameToId["meat"] = 2;

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

		public static InventoryItem getInventoryItemByName (string name)
        {
			foreach (InventoryItem i in items)
            {
				if (i.name == name)
                {
					return i;
                }
            }
			return null;
        }

		//private Dictionary<string, int> getIngredientsToInitializeItem (str)
        //{

        //}
	}
}
