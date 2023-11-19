using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : Interactable
{
    [SerializeField] Animator bubbleAnim;

    public override void OnInteract(Player p)
    {
        bubbleAnim.Play("Play");
    }
}
