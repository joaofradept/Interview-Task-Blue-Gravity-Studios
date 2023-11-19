using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteractableFound();

    void OnInteractableLost();

    void OnInteract(Player p);

    void OnInteractStop();
}
