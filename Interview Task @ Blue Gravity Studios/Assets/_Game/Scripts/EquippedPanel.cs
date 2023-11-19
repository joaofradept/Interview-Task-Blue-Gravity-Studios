using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquippedPanel : SimplePanel
{
    [SerializeField] PlayerBelongings playerBelongings;

    [SerializeField] Image[] equipmentImages;

    public void OnEquipmentUpdated(Purchasable[] equipment)
    {
        for (int i = 0; i < equipmentImages.Length; ++i)
        {
            if (equipment[i] != null)
            {
                equipmentImages[i].sprite = equipment[i].SpriteRenderer.sprite;
                equipmentImages[i].color = equipment[i].SpriteRenderer.color;
                equipmentImages[i].enabled = true;
            }
            else
            {
                equipmentImages[i].sprite = null;
                equipmentImages[i].color = Color.white;
                equipmentImages[i].enabled = false;
            }
        }
    }

    private void OnEnable()
    {
        playerBelongings.onEquipmentChanged += OnEquipmentUpdated;
    }

    private void OnDisable()
    {
        playerBelongings.onEquipmentChanged -= OnEquipmentUpdated;
    }
}
