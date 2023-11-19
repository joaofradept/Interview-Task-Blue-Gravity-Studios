using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    enum State { NONE, FOUND, INTERACTING }

    [SerializeField] GameObject interactIcon;

    State currentState;
    Interactable currentInteractable;

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        currentState = State.NONE;

        interactIcon.SetActive(false);
    }

    void Update()
    {
        if (currentState == State.FOUND)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentState = State.INTERACTING;

                currentInteractable.OnInteract(player);
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

            interactIcon.SetActive(true);

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

            interactIcon.SetActive(false);

            interactable.OnInteractableLost();
        }
    }
}
