using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPanel : SimplePanel
{
    [SerializeField] ItemUI shopItemPrefab;
    [SerializeField] Transform shopItemParent;
    [SerializeField] TMP_Text objectsName;
    [SerializeField] TMP_Text objectsDescription;
    [SerializeField] TMP_Text objectsCost;
    [SerializeField] GameObject noSelectionOverlay;
    [SerializeField] Animator purchaseResultAnim;
    [SerializeField] TMP_Text emptyShopText;

    Shop loadedShop;

    List<ItemUI> loadedUIItems;
    int currentSelectionIndex; // -1 if no selection

    public void SelectItem(int index)
    {
        DeselectCurrentItem();

        currentSelectionIndex = index;

        noSelectionOverlay.SetActive(false);

        Purchasable selectedPurchasable
            = loadedShop.Purchasables.List[index];

        objectsName.text = selectedPurchasable.Title;
        objectsDescription.text
            = selectedPurchasable.Description;
        objectsCost.text = selectedPurchasable.MarketValue.ToString();
    }

    // Called in the Inspector (See gameobject 'Buy Button')
    public void TryBuySelected()
    {
        Purchasable selectedPurchasable
            = loadedShop.Purchasables.List[currentSelectionIndex];

        if (loadedShop.TryProcessPurchase(selectedPurchasable))
            purchaseResultAnim.Play("Success");
        else
            purchaseResultAnim.Play("NoMoney");
    }

    // Also called in the Inspector (See gameobject 'Items')
    public void DeselectCurrentItem()
    {
        noSelectionOverlay.SetActive(true);

        if (currentSelectionIndex > -1)
        {
            var selectedItem
                = loadedUIItems[currentSelectionIndex];

            selectedItem.Deselect();

            currentSelectionIndex = -1;
        }
    }

    public override void Hide()
    {
        base.Hide();

        loadedShop?.ExitShop();

        loadedShop = null;
    }

    // Destroy current UI items (if exist)
    void DestroyUIItems()
    {
        if (loadedUIItems == null) return;

        foreach (var item in loadedUIItems)
            Destroy(item.gameObject);
    }

    // Load shop with purchasables
    public void LoadShop(Shop shop)
    {
        DestroyUIItems();

        loadedShop = shop;

        loadedUIItems = new List<ItemUI>();

        int indexInList = -1;
        foreach (var p in shop.Purchasables.List)
        {
            ++indexInList;

            var UIItem = Instantiate(shopItemPrefab, shopItemParent);

            UIItem.Initialize(p, SelectItem, indexInList);

            loadedUIItems.Add(UIItem);
        }

        currentSelectionIndex = -1;
        DeselectCurrentItem();

        // If there are no items in list,
        // show empty shop text.
        // Otherwise, hide it
        emptyShopText.gameObject.SetActive(indexInList == -1);
    }
}
