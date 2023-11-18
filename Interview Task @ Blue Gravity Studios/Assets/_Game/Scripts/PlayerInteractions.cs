using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    enum State { NONE, FOUND, INTERACTING }

    State currentState;
    Interactable currentInteractable;

    private void Start()
    {
        currentState = State.NONE;
    }

    void Update()
    {
        if (currentState == State.FOUND)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentState = State.INTERACTING;

                currentInteractable.OnInteract();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var interactable
            = collision.GetComponent<Interactable>();

        if (interactable && !currentInteractable)
        {
            currentInteractable = interactable;
            currentState = State.FOUND;

            currentInteractable.OnInteractableFound();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var interactable
            = collision.GetComponent<Interactable>();

        if (interactable &&
            interactable == currentInteractable)
        {
            currentInteractable = null;
            currentState = State.NONE;

            interactable.OnInteractableLost();
        }
    }
}
