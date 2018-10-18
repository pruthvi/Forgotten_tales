using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Etc, Consumable }
[CreateAssetMenu(menuName = "Forgotten Tales/Combat/Item")]
public class Item : ScriptableObject {

    private static int nextItemId;
    public int Id { get; private set; }
    public ItemType ItemType;
    public string Name;

    public Item(string name, ItemType type)
    {
        this.Id = nextItemId++;
        this.Name = name;
        this.ItemType = type;
    }

}
