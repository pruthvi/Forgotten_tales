using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager{

    public List<Item> Items { get; private set; }

	public InventoryManager()
    {
        Items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item != null)
        {
            Items.Add(item);
        }
    }

    public void RemoveItem(Item item)
    {
        foreach (Item i in Items)
        {
            if (i.Id == item.Id)
            {
                Items.Remove(i);
            }
        }
    }
}
