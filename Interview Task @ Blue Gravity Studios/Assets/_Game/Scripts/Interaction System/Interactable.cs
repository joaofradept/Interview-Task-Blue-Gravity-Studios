using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{

    public virtual void OnInteract(Player p)
    {
    }

    public virtual void OnInteractableFound()
    {
    }

    public virtual void OnInteractableLost()
    {
    }

    public virtual void OnInteractableStay()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnInteractStay(Player p)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnInteractStop()
    {
        throw new System.NotImplementedException();
    }
}
