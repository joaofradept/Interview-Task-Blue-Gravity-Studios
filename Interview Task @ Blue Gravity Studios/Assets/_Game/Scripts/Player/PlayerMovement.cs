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
    Vector2 lastMovement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GetSpritesheets(null);
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

            if (movementInput.x > 0)
                WalkRight();
            else
                WalkLeft();
        }
        if (movementInput.y != 0)
        {
            movement.y = movementInput.y;
            movement.x = 0;

            if (movementInput.y > 0)
                WalkUp();
            else
                WalkDown();
        }
        if (movementInput.sqrMagnitude <= 0.01f)
        {
            lastMovement = movement;

            if (lastMovement.x > 0)
                IdleRight();
            else
                IdleLeft();

            if (lastMovement.y > 0)
                IdleUp();
            else
                IdleDown();

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
        playerBelongings.onEquipmentChanged += GetSpritesheets;
    }

    private void OnDisable()
    {
        playerBelongings.onEquipmentChanged -= GetSpritesheets;
    }

    #region SPRITESHEETS_ANIM

    // Get spritesheets in children (also the player's)
    // This shall be called whenever spritesheets are added/removed
    void GetSpritesheets(Purchasable[] ps)
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

    void IdleUp()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.IdleUp();
        }
    }

    void IdleDown()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.IdleDown();
        }
    }

    void IdleLeft()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.IdleLeft();
        }
    }

    void IdleRight()
    {
        foreach (var item in spritesheetsAnim)
        {
            item.IdleRight();
        }
    }

    #endregion
}
