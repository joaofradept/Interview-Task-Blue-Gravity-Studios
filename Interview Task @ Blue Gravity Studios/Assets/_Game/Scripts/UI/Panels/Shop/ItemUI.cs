using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
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

    public void Initialize(Purchasable item,
        Action<int> onSelectItem, int indexInList)
    {
        SpriteRenderer spriteRenderer = item.SpriteRenderer;
        image.sprite = spriteRenderer.sprite;
        image.color = spriteRenderer.color;

        this.onSelectItem = onSelectItem;
        this.indexInList = indexInList;

        button.onClick.AddListener(delegate { Select(); });
    }

    // Show button as selected
    void Select()
    {
        onSelectItem.Invoke(indexInList);

        selectionOverlay.enabled = true;
    }

    // Show button as deselected
    public void Deselect()
    {
        selectionOverlay.enabled = false;
    }
}
