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

        playerBelongings.onBackpackChanged += LoadBackpack;

        objectInfoArea.SetActive(false);

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

        objectInfoArea.SetActive(true);

        Purchasable selectedItem
            = playerBelongings.Backpack.List[index];

        objectsName.text = selectedItem.Title;
    }

    // Also called in the Inspector (See gameobject 'Items')
    public void DeselectCurrentItem()
    {
        objectInfoArea.SetActive(false);

        if (currentSelectionIndex > -1)
        {
            var selectedItem
                = loadedUIItems[currentSelectionIndex];

            selectedItem.Deselect();

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
}
