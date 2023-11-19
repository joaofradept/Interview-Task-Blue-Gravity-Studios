using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : SimplePanel
{
    [SerializeField] PlayerBelongings playerBelongings;
    [SerializeField] ItemUI itemUIPrefab;
    [SerializeField] Transform itemUIParent;
    [SerializeField] TMP_Text objectsName;
    [SerializeField] GameObject objectInfoArea;
    [SerializeField] Animator sellResultAnim;
    [SerializeField] TMP_Text purchasableSellValue;
    [SerializeField] Button sellButton;

    List<ItemUI> loadedUIItems;
    int currentSelectionIndex; // -1 if no selection

    Shop enteredShop;

    public override void Show()
    {
        base.Show();

        // If backpack is updated, UI must reload backpack items
        playerBelongings.onBackpackChanged += LoadBackpack;

        // Hide item information panel
        objectInfoArea.SetActive(false);

        // Load items from backpack
        LoadBackpack();
    }

    public override void Hide()
    {
        base.Hide();

        playerBelongings.onBackpackChanged -= LoadBackpack;
    }

    // Select item and show its information
    public void SelectItem(int index)
    {
        // Deselect current item
        DeselectCurrentItem();

        currentSelectionIndex = index;

        // Show item information panel
        objectInfoArea.SetActive(true);

        Purchasable selectedItem
            = playerBelongings.Backpack.List[index];

        objectsName.text = selectedItem.Title;

        purchasableSellValue.text = (selectedItem.MarketValue / 2).ToString();
    }

    // Also called in the Inspector (See gameobject 'Items')
    public void DeselectCurrentItem()
    {
        // Hide item information panel
        objectInfoArea.SetActive(false);

        // Deselect current item if there is one selected
        if (currentSelectionIndex > -1)
        {
            // Get selected item
            var selectedItem
                = loadedUIItems[currentSelectionIndex];

            // Deselect it
            selectedItem.Deselect();

            // Set selection as none
            currentSelectionIndex = -1;
        }
    }

    // Destroy current UI items (if exist)
    void DestroyUIItems()
    {
        if (loadedUIItems == null) return;

        foreach (var item in loadedUIItems)
            Destroy(item.gameObject);
    }

    // Load backpack with purchasables
    void LoadBackpack()
    {
        DestroyUIItems();

        loadedUIItems = new List<ItemUI>();

        int indexInList = -1;
        foreach (var p in playerBelongings.Backpack.List)
        {
            ++indexInList;

            var UIItem = Instantiate(itemUIPrefab, itemUIParent);

            UIItem.Initialize(p, SelectItem, indexInList);

            loadedUIItems.Add(UIItem);
        }

        currentSelectionIndex = -1;
        DeselectCurrentItem();
    }

    // Called in the Inspector (See gameobject 'Equip Button')
    public void EquipCurrentPurchasable()
    {
        Purchasable selectedItem
            = playerBelongings.Backpack.List[currentSelectionIndex];

        playerBelongings.EquipPurchasable(selectedItem);
    }

    // Called in the Inspector (See gameobject 'Buy Button')
    public void TrySellSelected()
    {
        Purchasable selectedItem
            = playerBelongings.Backpack.List[currentSelectionIndex];

        if (enteredShop.TryBuyFromPlayer(selectedItem))
            sellResultAnim.Play("Success");
        else
            sellResultAnim.Play("NoMoney");
    }

    // Enable sell button
    public void EnableSellAction(Shop shop, Player player)
    {
        enteredShop = shop;
        sellButton.interactable = true;
    }

    // Disable sell button
    public void DisableSellAction(Shop shop, Player player)
    {
        enteredShop = null;
        sellButton.interactable = false;
    }

    private void OnEnable()
    {
        Shop.onPlayerEnteredShop += EnableSellAction;
        Shop.onPlayerExitedShop += DisableSellAction;
    }

    private void OnDisable()
    {
        Shop.onPlayerEnteredShop -= EnableSellAction;
        Shop.onPlayerExitedShop -= DisableSellAction;
    }
}
