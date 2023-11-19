using UnityEngine;

[CreateAssetMenu(fileName = "Purchasable", menuName = "Profiles/Purchasable")]
public class PurchasableProfile : ScriptableObject
{
    public Item item;
    public int price;
}