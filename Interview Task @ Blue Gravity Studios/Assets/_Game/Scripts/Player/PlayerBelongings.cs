using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBelongings : MonoBehaviour
{
    [SerializeField] Purchasable[] initialBackpackItems;
    [SerializeField] int backpackItemLimit;

    public Inventory<Purchasable> Backpack { get; private set; }

    public Action onBackpackChanged;

    private void Awake()
    {
        Backpack = new Inventory<Purchasable>
            (initialBackpackItems, backpackItemLimit);
    }

    public bool AddToBackpack(Purchasable item)
    {
        if (Backpack.AddToList(item))
        {
            onBackpackChanged?.Invoke();

            return true;
        }

        return false;
    }

    public void EquipItem(Purchasable item)
    {

    }
}
