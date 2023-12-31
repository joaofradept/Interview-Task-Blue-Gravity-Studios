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
    public void RemoveFromBackpack(Purchasable item)
    {
        // Unequip item if equipped
        if (equipment == item) EquipPurchasable(null);

        Backpack.RemoveFromList(item);

        onBackpackChanged?.Invoke();
    }

    // Equip purchasable
    // Pass null to unequip
    public void EquipPurchasable(Purchasable p)
    {
        // Destroy current
        if (equipment) Destroy(equipment.gameObject);

        // Instantiate new
        if (p != null)
            equipment = Instantiate(p, equipmentParent);
        else
            equipment = null;

        // Invoke action to alert that equipment was changed
        onEquipmentChanged?.Invoke(equipment);
    }
}
