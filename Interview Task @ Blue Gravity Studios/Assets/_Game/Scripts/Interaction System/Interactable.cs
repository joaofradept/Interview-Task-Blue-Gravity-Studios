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

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractableFound()
    {
        interactIcon.SetActive(true);
    }

    public void OnInteractableLost()
    {
        interactIcon.SetActive(false);
    }

    public void OnInteractableStay()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractStay(PlayerInteractions p)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteractStop()
    {
        throw new System.NotImplementedException();
    }
}
