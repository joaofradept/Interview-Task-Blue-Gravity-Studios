using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anim;

    Rigidbody2D rb;

    Vector2 movementInput;
    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get movement from input
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        if (movementInput.x != 0)
        {
            movement.x = movementInput.x;
            movement.y = 0;
        }
        if (movementInput.y != 0)
        {
            movement.y = movementInput.y;
            movement.x = 0;
        }
        if (movementInput.sqrMagnitude <= 0.01f)
        {
            movement.x = 0;
            movement.y = 0;
        }

        // If there's no movement at all, don't send it to animator
        // so that animator keeps info about the last direction
        // and uses it to set the idle animation
        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }

        // Set movement speed in animator
        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Apply movement to rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
