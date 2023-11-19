using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    [SerializeField] ShopPanel shopUI;
    [SerializeField] Purchasable[] initialPurchasables;


    public static Action<Shop, Player> onPlayerEnteredShop;
    public static Action<Shop, Player> onPlayerExitedShop;

    public Wallet ShopWallet { get; private set; }
    public Inventory<Purchasable> Purchasables { get; private set; }

    Player loadedPlayer;

    private void Awake()
    {
        Purchasables = new Inventory<Purchasable>
            (initialPurchasables);

        ShopWallet = GetComponent<Wallet>();
    }

    // This is called when player interacts with shop
    public override void OnInteract(Player p)
    {
        base.OnInteract(p);

        loadedPlayer = p;

        // Load shop's UI with the current shop
        shopUI.LoadShop(this);

        // Show it
        shopUI.Show();

        // Invoke event about shop being entered by a player
        onPlayerEnteredShop?.Invoke(this, loadedPlayer);
    }

    public override void OnInteractableLost()
    {
        base.OnInteractableLost();

        if (loadedPlayer)
        {
            // Unload player
            loadedPlayer = null;

            // Hide UI
            shopUI.Hide();
        }
    }

    // This is called by the UI when panel is closed
    public void ExitShop()
    {
        // Invoke event about shop being left by a player
        onPlayerExitedShop?.Invoke(this, loadedPlayer);
    }

    // Try to buy purchasable from player
    public bool TryBuyFromPlayer(Purchasable p)
    {
        int cost = p.MarketValue / 2;

        if (ShopWallet.TryWidthdraw(cost))
        {
            // Add money to player's wallet
            loadedPlayer.PlayerWallet.AddMoney(cost);

            // Remove purchasable from player
            loadedPlayer.Belongings.RemoveFromBackpack(p);

            // Add purchased item to shops's purchasables
            Purchasables.AddToList(p);

            // Reload UI
            shopUI.LoadShop(this);

            return true;
        }

        return false;
    }

    // Try to process purchase
    public bool TryProcessPurchase(Purchasable p)
    {
        int cost = p.MarketValue;

        if (loadedPlayer.PlayerWallet.TryWidthdraw(cost))
        {
            // Add money to shop's wallet
            ShopWallet.AddMoney(cost);

            // Remove purchasable from shop
            Purchasables.RemoveFromList(p);

            // Reload UI
            shopUI.LoadShop(this);

            // Add purchased item to player's backpack
            loadedPlayer.Belongings.AddToBackpack(p);

            return true;
        }

        return false;
    }
}
