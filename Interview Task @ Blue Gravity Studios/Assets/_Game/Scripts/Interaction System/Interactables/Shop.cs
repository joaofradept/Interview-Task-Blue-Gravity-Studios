using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    [SerializeField] ShopPanel shopUI;
    [SerializeField] PurchasableProfile[] initialPurchasables;

    public Wallet ShopWallet { get; private set; }
    public Inventory<PurchasableProfile> Purchasables { get; private set; }

    Player loadedPlayer;

    private void Awake()
    {
        Purchasables = new Inventory<PurchasableProfile>
            (initialPurchasables);

        ShopWallet = GetComponent<Wallet>();
    }

    public override void OnInteract(Player p)
    {
        base.OnInteract(p);

        loadedPlayer = p;

        shopUI.LoadShop(this);
        shopUI.Show();
    }

    public bool TryProcessPurchase
        (PurchasableProfile purchasable)
    {
        int cost = purchasable.price;

        if (loadedPlayer.PlayerWallet.TryWidthdraw(cost))
        {
            ShopWallet.AddMoney(cost);

            Purchasables.RemoveFromList(purchasable);

            shopUI.LoadShop(this);

            return true;
        }

        return false;
    }
}
