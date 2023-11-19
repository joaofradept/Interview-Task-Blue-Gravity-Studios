using UnityEngine;

[CreateAssetMenu(fileName = "Purchasable", menuName = "Profiles/Purchasable")]
public class PurchasableProfile : ScriptableObject
{
    public Purchasable item;
    public int price;
}