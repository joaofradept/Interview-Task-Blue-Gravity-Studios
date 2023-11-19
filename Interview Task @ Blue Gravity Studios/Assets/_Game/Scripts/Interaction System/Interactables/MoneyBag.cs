using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBag : Interactable
{
    [SerializeField] int amount;

    public override void OnInteract(Player p)
    {
        p.PlayerWallet.AddMoney(amount);

        Destroy(gameObject);
    }
}
