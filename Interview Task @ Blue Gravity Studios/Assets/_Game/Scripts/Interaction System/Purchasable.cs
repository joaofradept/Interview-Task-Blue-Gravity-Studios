using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : MonoBehaviour
{

    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] int marketValue;

    public string Title => title;
    public string Description => description;
    public SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

    public int MarketValue => marketValue;
}
