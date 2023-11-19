using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryPanel : SimplePanel
{
    [SerializeField] PlayerBelongings playerBelongings;
    [SerializeField] ItemUI itemUIPrefab;
    [SerializeField] Transform itemUIParent;
    [SerializeField] TMP_Text objectsName;
    [SerializeField] GameObject objectInfoArea;
    [SerializeField] Animator sellResultAnim;

    List<ItemUI> loadedUIItems;
    int currentSelectionIndex; // -1 if no selection

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
    }

    // Called in the Inspector (See gameobject 'Equip Button')
    public void EquipCurrentPurchasable()
    {
        Purchasable selectedItem
            = playerBelongings.Backpack.List[currentSelectionIndex];

        playerBelongings.EquipPurchasable(selectedItem);
    }
}
