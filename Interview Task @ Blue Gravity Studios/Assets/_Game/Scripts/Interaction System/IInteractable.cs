using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnInteractableFound();

    void OnInteractableLost();

    void OnInteractableStay();

    void OnInteract();

    void OnInteractStay(PlayerInteractions p);

    void OnInteractStop();
}
