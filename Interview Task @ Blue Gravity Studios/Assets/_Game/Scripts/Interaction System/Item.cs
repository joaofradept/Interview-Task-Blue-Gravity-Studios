using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { HAT, HAIR, BODY }

    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] Type type;

    public string Title => title;
    public string Description => description;
    public Sprite Image => GetComponent<SpriteRenderer>().sprite;

    public void PlayAnimation()
    {

    }
}
