using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Etc, Consumable }
[CreateAssetMenu(menuName = "Forgotten Tale/Combat/Item")]
public class Item : ScriptableObject {

    public AudioClip NameClip;
    public ItemType ItemType;
    public int Quantity;
    public int MaxStack;

}
