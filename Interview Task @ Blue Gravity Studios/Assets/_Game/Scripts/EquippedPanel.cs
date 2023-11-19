using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedPanel : SimplePanel
{
    [SerializeField] PlayerBelongings playerBelongings;

    [SerializeField] Image equipmentImage;
    [SerializeField] GameObject equipmentItem;

    private void Start()
    {
        OnEquipmentUpdated(null);
    }

    // Update UI with given equipment
    public void OnEquipmentUpdated(Purchasable equipment)
    {
        if (equipment != null)
        {
            equipmentItem.SetActive(true);
            equipmentImage.sprite = equipment.SpriteRenderer.sprite;
            equipmentImage.color = equipment.SpriteRenderer.color;
        }
        else
            equipmentItem.SetActive(false);
    }

    void OnEnable()
    {
        // When equipment is changed, update UI
        playerBelongings.onEquipmentChanged += OnEquipmentUpdated;
    }

    void OnDisable()
    {
        playerBelongings.onEquipmentChanged -= OnEquipmentUpdated;
    }
}
