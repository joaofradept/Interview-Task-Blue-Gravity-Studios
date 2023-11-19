using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : MonoBehaviour
{
    public enum WearType { HAT, HAIR, BODY }

    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] int marketValue;
    [SerializeField] WearType wearType;

    public string Title => title;
    public string Description => description;
    public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

    public int MarketValue => marketValue;
    public WearType Type => wearType;
}
