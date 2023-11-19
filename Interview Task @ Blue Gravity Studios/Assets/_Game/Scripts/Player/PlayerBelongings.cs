using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBelongings : MonoBehaviour
{
    [SerializeField] Purchasable[] initialBackpackItems;
    [SerializeField] int backpackItemLimit;
    [SerializeField] Transform equipmentParent;

    Purchasable equippedHat;
    Purchasable equippedHair;
    Purchasable equippedBody;

    public Inventory<Purchasable> Backpack { get; private set; }

    public Action onBackpackChanged;
    public Action<Purchasable[]> onEquipmentChanged;

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

    public bool RemoveFromBackpack(Purchasable item)
    {
        if (Backpack.AddToList(item))
        {
            onBackpackChanged?.Invoke();

            return true;
        }

        return false;
    }

    public void EquipPurchasable(Purchasable p)
    {
        switch (p.Type)
        {
            case Purchasable.WearType.HAT:
                equippedHat = Instantiate(p, equipmentParent);
                break;
            case Purchasable.WearType.HAIR:
                equippedHair = Instantiate(p, equipmentParent);
                break;
            case Purchasable.WearType.BODY:
                equippedBody = Instantiate(p, equipmentParent);
                break;
        }

        onEquipmentChanged?.Invoke(new Purchasable[3]
        { equippedHat, equippedHair, equippedBody });
    }
}
