using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePanel : MonoBehaviour
{
    Animator anim;

    bool isVisible;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Show()
    {
        if (isVisible) return;

        anim.SetTrigger("Show");

        isVisible = true;
    }

    public virtual void Hide()
    {
        if (!isVisible) return;

        anim.SetTrigger("Hide");

        isVisible = false;
    }
}
