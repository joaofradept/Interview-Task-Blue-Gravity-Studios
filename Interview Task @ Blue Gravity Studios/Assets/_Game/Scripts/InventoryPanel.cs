using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryPanel : SimplePanel
{
    [SerializeField] PlayerBelongings playerBelongings;
    [SerializeField] InventoryItem inventoryItemPrefab;
    [SerializeField] Transform inventoryItemParent;
    [SerializeField] TMP_Text objectsName;
    [SerializeField] TMP_Text objectsDescription;
    [SerializeField] GameObject noSelectionOverlay;
    [SerializeField] Animator purchaseResultAnim;
    [SerializeField] TMP_Text emptyText;

    List<InventoryItem> loadedUIItems;
    int currentSelectionIndex; // -1 if no selection

    public override void Show()
    {
        base.Show();

        playerBelongings.onBackpackChanged += LoadBackpack;

        LoadBackpack();
    }

    public override void Hide()
    {
        base.Hide();

        playerBelongings.onBackpackChanged -= LoadBackpack;
    }

    public void SelectItem(int index)
    {
        DeselectCurrentItem();

        currentSelectionIndex = index;

        noSelectionOverlay.SetActive(false);

        Item selectedItem
            = playerBelongings.Backpack.List[index];

        objectsName.text = selectedItem.Title;
        objectsDescription.text
            = selectedItem.Description;
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

    void LoadBackpack()
    {
        loadedUIItems = new List<InventoryItem>();

        int indexInList = -1;
        foreach (var p in playerBelongings.Backpack.List)
        {
            ++indexInList;

            var UIItem = Instantiate(inventoryItemPrefab, inventoryItemParent);

            UIItem.Initialize(p, SelectItem, indexInList);

            loadedUIItems.Add(UIItem);
        }
    }
}
