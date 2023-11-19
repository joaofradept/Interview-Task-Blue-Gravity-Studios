using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesheetAnimator : MonoBehaviour
{
    [SerializeField] string spriteName;
    [SerializeField] float timeBetweenSprites;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Use commas to separate indexes.")]
    [Header("Indexes correspond to frames in the spritesheet.")]
    [Tooltip("Indexes for the Walk Up animation.")]
    public string sWalkUpAnimIndexes;
    [Tooltip("Indexes for the Walk Down animation.")]
    public string sWalkDownAnimIndexes;
    [Tooltip("Indexes for the Walk Left animation.")]
    public string sWalkLeftAnimIndexes;
    [Tooltip("Indexes for the Walk Right animation.")]
    public string sWalkRightAnimIndexes;

    [Tooltip("Indexes for the Idle animation.")]
    public string sIdleAnimIndexes;

    SpritesheetLoader spritesheetLoader;

    Sprite[] walkUp;
    Sprite[] walkDown;
    Sprite[] walkLeft;
    Sprite[] walkRight;

    Sprite[] idle;

    // I prefered to write this script instead of
    // using Animator State Machine because there's
    // a pattern accross different spritesheets
    // (in what concerns the organisation of their animations
    // inside the spritesheets)
    // in the downloaded asset package
    void Awake()
    {
        spritesheetLoader = new SpritesheetLoader(spriteName);

        // Get sprite arrays from spritesheet for each animation,
        // according to specified indexes

        // Walk sprites
        walkUp = spritesheetLoader.GetSpriteArrayFromIndexes(sWalkUpAnimIndexes);
        walkDown = spritesheetLoader.GetSpriteArrayFromIndexes(sWalkDownAnimIndexes);
        walkLeft = spritesheetLoader.GetSpriteArrayFromIndexes(sWalkLeftAnimIndexes);
        walkRight = spritesheetLoader.GetSpriteArrayFromIndexes(sWalkRightAnimIndexes);

        // Idle sprites
        idle = spritesheetLoader.GetSpriteArrayFromIndexes(sIdleAnimIndexes);
    }

    void SetAnimation(ref Sprite[] spriteGroup)
    {
        if (currentSpriteGroup != spriteGroup)
            currentAnimationIndex = 0;

        currentSpriteGroup = spriteGroup;
    }

    public void WalkUp()
    {
        SetAnimation(ref walkUp);
    }

    public void WalkDown()
    {
        SetAnimation(ref walkDown);
    }

    public void WalkLeft()
    {
        SetAnimation(ref walkLeft);
    }

    public void WalkRight()
    {
        SetAnimation(ref walkRight);
    }

    public void Idle()
    {
        SetAnimation(ref idle);
    }

    int currentAnimationIndex;
    Sprite[] currentSpriteGroup;

    public void StartAnimation()
    {
        // Start with Idle
        Idle();

        // Start animating
        StartCoroutine(Animation_SpriteRenderer());
    }

    // This coroutine plays indefinitely
    // It plays current SpriteGroup in its sequence
    IEnumerator Animation_SpriteRenderer()
    {
        while (true)
        {
            // If reached last sprite in group, repeat loop
            if (currentAnimationIndex == currentSpriteGroup.Length)
                currentAnimationIndex = 0;

            // Set renderer's sprite
            spriteRenderer.sprite = currentSpriteGroup[currentAnimationIndex];

            // Wait specified time
            yield return new WaitForSeconds(timeBetweenSprites);

            // Go to next sprite
            currentAnimationIndex++;
        }
    }
}
