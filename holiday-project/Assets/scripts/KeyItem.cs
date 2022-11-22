using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "keyItem", menuName = "ScriptableObjects/KeyItem", order = 1)]
public class KeyItem : ScriptableObject
{
    public string itemName;

    public string description;

    public int itemID;

    public Sprite icon;

    public bool useable;

    public bool equippable;
}
