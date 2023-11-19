using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBelongings : MonoBehaviour
{
    [SerializeField] Item[] initialBackpackItems;
    [SerializeField] int backpackItemLimit;

    Inventory<Item> backpack;

    private void Awake()
    {
        backpack = new Inventory<Item>
            (initialBackpackItems, backpackItemLimit);
    }

    public bool AddToBackpack(Item item)
    {
        if (backpack.AddToList(item))
        {


            return true;
        }

        return false;
    }

    public void UseItemFromBackpack()
    {

    }
}
