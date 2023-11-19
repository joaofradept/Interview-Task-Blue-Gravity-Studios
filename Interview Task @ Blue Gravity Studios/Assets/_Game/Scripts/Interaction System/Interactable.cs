using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public GameObject interactIcon;

    void Start()
    {
        interactIcon.SetActive(false);
    }

    public virtual void OnInteract(Player p)
    {
    }

    public virtual void OnInteractableFound()
    {
        interactIcon.SetActive(true);
    }

    public virtual void OnInteractableLost()
    {
        interactIcon.SetActive(false);
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
