using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBelongings : MonoBehaviour
{
    [SerializeField] Item[] initialBackpackItems;
    [SerializeField] int backpackItemLimit;

    public Inventory<Item> Backpack { get; private set; }

    public Action onBackpackChanged;

    private void Awake()
    {
        Backpack = new Inventory<Item>
            (initialBackpackItems, backpackItemLimit);
    }

    public bool AddToBackpack(Item item)
    {
        if (Backpack.AddToList(item))
        {
            onBackpackChanged?.Invoke();

            return true;
        }

        return false;
    }

    public void EquipItem(Item item)
    {

    }
}
