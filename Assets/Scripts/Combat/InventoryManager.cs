using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager{

    public List<Item> Items { get; private set; }

    private int _maxItemSlot = 4;
    public int MaxItemSlot
    {
        get
        {
            return _maxItemSlot;
        }
    }

    public InventoryManager()
    {
        Items = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        int index = indexOf(item);
        // If item does not exist
        if (index == -1)
        {
            Items.Add(item);
            return true;
        }
        else
        {
            // If item exists, check if quantity reach the max stack of the item
            if (Items[index].Quantity + 1 <= Items[index].MaxStack)
            {
                // If not increase the quantity
                Items[index].Quantity++;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        int index = indexOf(item);
        if (index != 1)
        {
            Items.RemoveAt(index);
            return true;
        }
        return false;
    }

    private int indexOf(Item item)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].name == item.name)
            {
                return i;
            }
        }
        return -1;
    }
}
