using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] string description;

    public string Title => title;
    public string Description => description;
    public Sprite Image => GetComponent<SpriteRenderer>().sprite;

    public void PlayAnimation()
    {

    }
}
