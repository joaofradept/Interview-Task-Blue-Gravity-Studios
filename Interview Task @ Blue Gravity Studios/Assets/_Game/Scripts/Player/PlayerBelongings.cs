using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBelongings : MonoBehaviour
{
    [SerializeField] Purchasable[] initialBackpackItems;
    [SerializeField] int backpackItemLimit;
    [SerializeField] Transform equipmentParent;

    Purchasable equipment;

    public Inventory<Purchasable> Backpack { get; private set; }

    public Action onBackpackChanged;
    public Action<Purchasable> onEquipmentChanged;

    private void Awake()
    {
        // Initialize backpack's inventory
        Backpack = new Inventory<Purchasable>
            (initialBackpackItems, backpackItemLimit);
    }

    // Add purchasable to backpack
    public bool AddToBackpack(Purchasable item)
    {
        if (Backpack.AddToList(item))
        {
            onBackpackChanged?.Invoke();

            return true;
        }

        return false;
    }

    // Remove purchasable from backpack
    public bool RemoveFromBackpack(Purchasable item)
    {
        if (Backpack.AddToList(item))
        {
            onBackpackChanged?.Invoke();

            return true;
        }

        return false;
    }

    // Equip purchasable
    public void EquipPurchasable(Purchasable p)
    {
        // Destroy current
        if (equipment) Destroy(equipment.gameObject);

        // Instantiate new
        equipment = Instantiate(p, equipmentParent);

        // Invoke action to alert that equipment was changed
        onEquipmentChanged?.Invoke(equipment);
    }
}
