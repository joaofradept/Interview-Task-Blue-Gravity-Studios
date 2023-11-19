using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] Image selectionOverlay;
    [SerializeField] Image image;

    Button button;
    int indexInList;

    Action<int> onSelectItem;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void Initialize(Item item,
        Action<int> onSelectItem, int indexInList)
    {
        image.sprite = item.Image;

        var instantiatedItem = Instantiate(item, transform);

        instantiatedItem.transform.SetAsFirstSibling();

        this.onSelectItem = onSelectItem;
        this.indexInList = indexInList;

        button.onClick.AddListener(delegate { Select(); });
    }

    void Select()
    {
        onSelectItem.Invoke(indexInList);

        selectionOverlay.enabled = true;
    }

    public void Deselect()
    {
        selectionOverlay.enabled = false;
    }
}
