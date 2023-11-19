using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] PlayerBelongings playerBelongings;

    Rigidbody2D rb;
    SpritesheetAnimator[] spritesheetsAnim;

    Vector2 movementInput;
    Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Initialize spritesheets.
        // No need to pass arguments
        GetSpritesheets(null);
    }

    void Update()
    {
        // Get movement from input
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        if (movementInput.x != 0)
        {
            // Set movement in X axis
            movement.x = movementInput.x;
            movement.y = 0;

            // Apply animations
            if (movementInput.x > 0)
                WalkRight();
            else
                WalkLeft();
        }
        if (movementInput.y != 0)
        {
            // Set movement in Y axis
            movement.y = movementInput.y;
            movement.x = 0;

            // Apply animations
            if (movementInput.y > 0)
                WalkUp();
            else
                WalkDown();
        }
        if (movementInput.sqrMagnitude <= 0.01f)
        {
            // Apply animations
            Idle();

            movement.x = 0;
            movement.y = 0;
        }
    }

    private void FixedUpdate()
    {
        // Apply movement to rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        // If equipment changes, spritesheets must be reloaded
        // so to include all equipped spritesheet animations too
        playerBelongings.onEquipmentChanged += GetSpritesheets;
    }

    private void OnDisable()
    {
        playerBelongings.onEquipmentChanged -= GetSpritesheets;
    }

    #region SPRITESHEETS_ANIM

    // Get spritesheets in children (also the player's)
    // This shall be called whenever spritesheets are added/removed
    void GetSpritesheets(Purchasable p)
    {
        spritesheetsAnim
            = GetComponentsInChildren<SpritesheetAnimator>();

        foreach (var item in spritesheetsAnim)
        {
            item.StartAnimation();
        }
    }

    void WalkUp()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.WalkUp();
        }
    }

    void WalkDown()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.WalkDown();
        }
    }

    void WalkLeft()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.WalkLeft();
        }
    }

    void WalkRight()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.WalkRight();
        }
    }

    void Idle()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.Idle();
        }
    }

    #endregion
}
