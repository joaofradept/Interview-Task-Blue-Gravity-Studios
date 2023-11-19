using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    [SerializeField] ShopPanel shopUI;
    [SerializeField] Purchasable[] initialPurchasables;

    public Wallet ShopWallet { get; private set; }
    public Inventory<Purchasable> Purchasables { get; private set; }

    Player loadedPlayer;

    private void Awake()
    {
        Purchasables = new Inventory<Purchasable>
            (initialPurchasables);

        ShopWallet = GetComponent<Wallet>();
    }

    public override void OnInteract(Player p)
    {
        base.OnInteract(p);

        loadedPlayer = p;

        // Load shop's UI with the current shop
        shopUI.LoadShop(this);

        // Show it
        shopUI.Show();
    }

    // Try to process purchase
    public bool TryProcessPurchase
        (Purchasable purchasable)
    {
        int cost = purchasable.MarketValue;

        if (loadedPlayer.PlayerWallet.TryWidthdraw(cost))
        {
            // Add money to shop's wallet
            ShopWallet.AddMoney(cost);

            // Remove purchasable from shop
            Purchasables.RemoveFromList(purchasable);

            // Reload UI
            shopUI.LoadShop(this);

            // Add purchased item to player's backpack
            loadedPlayer.Belongings.AddToBackpack(purchasable);

            return true;
        }

        return false;
    }
}
